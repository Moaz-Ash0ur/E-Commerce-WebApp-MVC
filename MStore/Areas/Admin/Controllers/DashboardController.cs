using ECommerceStore.BLL;
using ECommerceStore.BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MStore.Areas.Admin.Models;
using static ECommerceStore.Entities.Order;

namespace MStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]

    [Area("Admin")]
    public class DashboardController : Controller
    {

        private IProductService _productService;
        private IOrderService _orderService;
        private IUserService _userService;

        public DashboardController(IProductService productService, IOrderService orderService = null, IUserService userService = null)
        {
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
        }


        public async Task<IActionResult> Index()
        {

          var users =  await _userService.GetAllUsersAsync(); 
          int totalProducts = _productService.GetAll().Count();
          var totalUser = users.Where(u => !(u.Email!.Contains("admin") || u.UserName!.Contains("admin"))).Count();


         var  totalOrders = _orderService.GetAll()
            .Where(o => o.orderStatus == OrderStatus.Delivered).Count();

             var last7Days = Enumerable.Range(0, 7)
            .Select(i => DateTime.Today.AddDays(-i))
            .Reverse()
            .ToList();
            

            var sales = _orderService.GetAll()//order list of days after samll day in 7
                .Where(o => o.CreatedAt >= last7Days.First())//big order date of small day from 7
                .ToList();

            var salesData = last7Days.Select(day =>
            { 
                var total = sales
                    .Where(o => o.CreatedAt.Date == day.Date)
                    .Sum(o => o.TotalAmount);

                return new
                {
                    Date = day.ToString("yyyy-MM-dd"),
                    Total = total
                };
            }).ToList();

            var vm = new DashboardVM
            {
                TotalProducts = totalProducts,
                TotalOrders = totalOrders,
                TotalUsers = totalUser,

                TotalSales = salesData.Sum(x => x.Total),
                SalesDates = salesData.Select(x => x.Date).ToList(),
                SalesTotals = salesData.Select(x => x.Total).ToList()
            };

            return View(vm);
        }

       

    }
}
