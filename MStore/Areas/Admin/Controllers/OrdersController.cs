using ECommerceStore.BLL.Interface;
using ECommerceStore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MStore.Areas.Admin.Models;
using MStore.Models;
using MStore.Properties;
using static ECommerceStore.Entities.Order;

namespace MStore.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin")]
    public class OrdersController : Controller
    {

        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index(int pageNumber = 1)
        {
          
            (var orders, var totalPages) = _orderService.PaginateOrders(pageNumber, Convert.ToInt32(ResGeneral.PageSize));

            ViewBag.TotalPages = totalPages;
            ViewBag.PageNumber = pageNumber;

            return View(orders.ToList());
        }

        public IActionResult Details(int id) {

            PaymentOrderList();

            var orderDetails = _orderService.GetAllAsQueryable()
                .Where(o => o.Id == id).FirstOrDefault();

               return View(orderDetails);
        }


        //list of order status 
        // modal to show then
        //sent id order to update it
        [HttpPost]
        public IActionResult Edit(EditOrderStatus model)
        {
            PaymentOrderList();

            var order = _orderService.GetById(model.Id);

            if(order != null)
            {        
                
                order.paymentStatus = model.paymentStatus;         
                order.orderStatus = model.orderStatus;
                
                _orderService.Update(order);
            }

            return RedirectToAction("Details", new { id = model.Id });
        }

        public void PaymentOrderList()
        {

            ViewBag.PaymentStatusList = Enum.GetValues(typeof(PaymentStatus))
              .Cast<PaymentStatus>()
              .Select(p => new SelectListItem
              {
                  Text = p.ToString(),
                  Value = ((int)p).ToString()
              }).ToList();


            ViewBag.OrderStatusList = Enum.GetValues(typeof(OrderStatus))
             .Cast<OrderStatus>()
             .Select(o => new SelectListItem
             {
                 Text = o.ToString(),
                 Value = ((int)o).ToString()
             }).ToList();        
        }





    }
}
