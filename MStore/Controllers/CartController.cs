using ECommerceStore.BLL;
using ECommerceStore.BLL.Interface;
using ECommerceStore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using MStore.Models;
using MStore.Properties;
using static ECommerceStore.Entities.Order;

namespace MStore.Controllers
{
    public class CartController : Controller
    {

        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public CartController(ICartService cartService, IProductService productService, IOrderService orderService, IUserService userService)
        {
            _cartService = cartService;
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
        }


        //Show Shopping Cart ,Add Update delete on in
        public IActionResult Index()
        {
            var items = _cartService.GetCartItems();
            return View(items);
        }

        private void AddItemToCart(List<CartItem>existingItems, int productId)
        {
            var product = _productService.GetById(productId);
            if (product == null)
                return;

            var existing = existingItems.FirstOrDefault(x => x.ProductId == productId);
            int quantity = existing != null ? existing.Quantity + 1 : 1;

            if (existing != null)
            {
                existing.Quantity = quantity;
            }
            else
            {
                existingItems.Add(new CartItem
                {
                    ProductId = product!.Id,
                    ProductName = product.Name,
                    ImageName = product.ImageName,
                    UnitPrice = product.Price,
                    Quantity = quantity
                });
            }
        }


        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var existingItems = _cartService.GetCartItems();
            AddItemToCart(existingItems,productId);

            _cartService.SaveCartItems(existingItems);

            var totalCount = existingItems.Sum(x => x.Quantity);
            return Json(new { success = true, count = totalCount });
        }

        [HttpPost]
        public IActionResult Remove(int productId)
        {
            //to see the last update direct you should read the vlaue from the current cokkies you update or delete from it
            //dont depand on the GetCartItem() beacuse not save new cokkie dreict
            //calc the size from the same code for edit oe update the new cokkies will bw safe

            var cart = _cartService.GetCartItems();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {             
                cart.Remove(item);
            }

            _cartService.SaveCartItems(cart);
            var totalCount = cart.Sum(x => x.Quantity);

            return Json(new { success = true , count = totalCount });
        }
      
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int change)
        {
            var cart = _cartService.GetCartItems();
            var item = cart.FirstOrDefault(i => i.ProductId == productId);


            if (item != null)
            {
                item.Quantity += change;               
            }

            _cartService.SaveCartItems(cart);
            var totalCount = cart.Sum(x => x.Quantity);

            return Json(new { newQty = item?.Quantity ?? 0 , count =  totalCount});
        }

        public void getPaymentMethodList()
        {

            //        ViewBag.PaymentMethodList = Enum.GetValues(typeof(PaymentMethod))
            //.Cast<PaymentMethod>()
            //.Select(e => new SelectListItem
            //{
            //    Text = e.ToString(),
            //    Value = ((int)e).ToString()
            //}).ToList();


            List<string> paymentMethodList = new List<string>() { "CashOnDelivery", "PayPal", "CreditCard" };
            SelectList selectListItems = new SelectList(paymentMethodList);
            ViewBag.PaymentMethodList = selectListItems;

        }



        //Confirm Order

        [Authorize]
        public IActionResult Confirm()
        {

            if(_cartService.GetCartItems() == null || !_cartService.GetCartItems().Any())
            {              
               TempData["ErrorMessage"] = "Cart Is Empty !";
               return RedirectToAction("Index");           
            }

            getPaymentMethodList();

            var subTotal = _cartService.GetCartItems().Sum(item => item.UnitPrice * item.Quantity);
            var finalTotal = subTotal + Convert.ToDecimal(ResGeneral.ShippingFee);

            ViewBag.SubTotal = subTotal;
            ViewBag.TotalPrice = finalTotal;
            return View(new CheckOutVm());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(CheckOutVm CheckOutVm)
        {

            var cart = _cartService.GetCartItems();

            if (cart == null || !cart.Any())
            {
                TempData["ErrorMessage"] = "Cart Is Empty !";
                return RedirectToAction("Index");
            }
            var customer = await _userService.GetUserAsync(User);

            var subTotal = _cartService.GetCartItems().Sum(item => item.UnitPrice * item.Quantity);
            var finalTotal = subTotal + Convert.ToDecimal(ResGeneral.ShippingFee);


            Order order = new Order
            {
                ClientId = customer!.Id,
                paymentMethod = Enum.Parse<PaymentMethod>(CheckOutVm.paymentMethod),
                paymentStatus = Order.PaymentStatus.Pending,
                DeliveryAddress = CheckOutVm.DeliveryAddress,
                ShippingFee = Convert.ToDecimal(ResGeneral.ShippingFee),
                PaymentDetails = "",
                CreatedAt = DateTime.Now,
                orderStatus = Order.OrderStatus.Created,
                TotalAmount = finalTotal,
                Items = cart.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };


            _orderService.Create(order);
            _cartService.ClearCart();
            return RedirectToAction("ThanksForOrder", new { id = order.Id });
        }

       public IActionResult ThanksForOrder(int id)
       {
            ViewBag.OrderId = id;
            return View("Thanks");
       }


    }
}
