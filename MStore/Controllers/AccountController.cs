using ECommerceStore.BLL;
using ECommerceStore.BLL.DTOs;
using ECommerceStore.Entities;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MStore.Models;
using System.ComponentModel.DataAnnotations;

namespace MStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        //Register anfd login
        public IActionResult Register()
        {        
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            
            var newUser = new UserRegister
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                Password = registerModel.Password,
                PhoneNumber = registerModel.PhoneNumber,
                Address = registerModel.Address
            };

            var result = await _userService.RegisterAsync(newUser);

            if (result.Succeeded)
                return RedirectToAction("Index","Home");


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerModel);
        }

        public IActionResult Logout() { 
                  
            _userService.LogoutAsync();
            return RedirectToAction("Index","Home");       
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login loginInfo , string returnUrl = null)
        {           

            var result = await _userService.LoginAsync(loginInfo.Email, loginInfo.Password , loginInfo.RememberMe);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid login attempt.";
            }

            return View(loginInfo);
        }
      
        //Show & Update User Profile
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userService.GetUserAsync(User);

            if (user == null)
                return NotFound();

            var userProfile = new UserProfileVm
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                CreatedAt = user.CreatedAt
            };

            return View(userProfile);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserProfileVm userProfile)
        {

            var user = await _userService.GetUserAsync(User);
          
            user!.FirstName = userProfile.FirstName;
            user.LastName = userProfile.LastName;
            user.UserName = userProfile.Email;
            user.Email = userProfile.Email;
            user.PhoneNumber = userProfile.PhoneNumber;
            user.Address = userProfile.Address;
   

            var result = await _userService.UpdateUserAsync(user);

            if (result.Succeeded)
                TempData["SuccessMessage"] = "Profile updated successfully!";
            else
                TempData["ErrorMessage"] = string.Join("; ", result.Errors.Select(e => e.Description));


            return RedirectToAction("Profile");
        }

        public IActionResult AccessDenied()
        {
            //or can make a view show error to access
            return RedirectToAction("Index","Home");
        }


        //Update Password

        [Authorize]
        public IActionResult EditPassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditPassword(UpdatePasswordVm Pass)
        {

            var user = await _userService.GetUserAsync(User);

            var result = await _userService.ChangePasswordAsync(user,
                Pass.CurrentPassword , Pass.NewPassword);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Password updated successfully!";
            }
            else
            {
                ViewBag.ErrorMessage = "Error: " + result.Errors.First().Description;
                return View();
            }

            return RedirectToAction("Profile");
        }


        //method get for check use ajax call
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var isExists = await _userService.IsEmailExists(email);
            var currentUser = await _userService.GetUserAsync(User);

            if (currentUser != null)
            {
                if (isExists && currentUser!.Email != email)
                {
                    return Json($"email was already used !");
                }
            }
            else
            {
                if (isExists)
                {
                    return Json($"email was already used !");
                }
            }

                return Json(true);
        }


        //Reset Password View()
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgotPassword([EmailAddress,Required]string email)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var result = await _userService.SendPasswordResetLink(email, baseUrl);

            if(result)
             ViewBag.SuccessMessage = "Please check your Email account and click on the Password Reset link!✅";

            else
              ViewBag.ErrorMessage = "If the email you entered is registered with us, you will receive a password reset link shortly. Please check your inbox and spam folder.\r\n\r\n!";

            ViewBag.Email = email;

            return View();
        }


        [HttpGet]//the link show when from email recive client to reset password
        public IActionResult ResetPassword(string email, string token)
        {
            return View(new ResetPasswordDto { Email = email, Token = token });
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _userService.ResetPasswordAsync(dto);
            if(result.Succeeded)
            return  RedirectToAction("ResetPasswordSuccess");

            ViewBag.ErrorMessage = result.Errors.First().Description;

            return View();
        }

        public IActionResult ResetPasswordSuccess()
        {        
            return View();
        }



    }
}
