using ECommerceStore.BLL;
using ECommerceStore.BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MStore.Areas.Admin.Models;
using MStore.Properties;
using System.Data;
using System.Threading.Tasks;

namespace MStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var pageSize = Convert.ToInt32(ResGeneral.PageSize);

            (var usersDetails, var totalPages) = _userService.PaginateUsers(pageNumber,pageSize);

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(usersDetails.ToList());
        }


        public async Task<IActionResult> Details(string id)
        {

            if (id == null)
            {
                return RedirectToAction("Index", "Users");
            }

            var user = await _userService.GetUserByIdAsync(id);

            if (user != null)
            {
                var userVm = new UserViewModel
                {
                    Id  = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    Phone = user.PhoneNumber!,
                    Address = user.Address,
                    Roles = await _userService.GetUserRoles(user),
                    CreatedAt = user.CreatedAt,
                };
                return View(userVm);
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(string id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var appUser = await _userService.GetUserByIdAsync(id);

            if (appUser == null)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userService.GetUserAsync(User);
            if (currentUser!.Id == appUser.Id)
            {
                TempData["ErrorMessage"] = "You cannot delete your own account!";
                return RedirectToAction("Details", new { id });
            }

            var result = await _userService.DeleteUserAsync(appUser);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Unable to delete this account: " + result.Errors.First().Description;
            return RedirectToAction("Details", "Users", new { id });
        }




    }
}
