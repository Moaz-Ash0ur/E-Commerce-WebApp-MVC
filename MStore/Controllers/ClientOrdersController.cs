using ECommerceStore.BLL;
using ECommerceStore.BLL.Interface;
using ECommerceStore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MStore.Properties;

namespace MStore.Controllers
{
    [Authorize(Roles = "customer")]
    public class ClientOrdersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public ClientOrdersController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {

           var currentUser = await _userService.GetUserAsync(User);
            List<Order> myOrders = new();

            if (currentUser != null)
            {
                myOrders = _orderService.GetAllInclude("Items")
                               .Where(o => o.ClientId == currentUser!.Id).OrderByDescending(o => o.CreatedAt).ToList();
            }
         
            return View(myOrders);
        }


        public IActionResult Details(int id)
        {
            var orderDetails = _orderService.GetAllAsQueryable()
                .FirstOrDefault(o => o.Id == id);


            return View(orderDetails);
        }








    }
}
