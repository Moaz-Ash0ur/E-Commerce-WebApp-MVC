using ECommerceStore.BLL.DTOs;
using ECommerceStore.BLL.Interface;
using ECommerceStore.Entities;
using ECommerceStore.Entities.Views;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using NuGet.Common;
using System.Security.Claims;
using System.Security.Policy;

namespace ECommerceStore.BLL
{ 
   public class UserService : IUserService
    {

         private readonly UserManager<ApplicationUser> _userManager;
         private readonly SignInManager<ApplicationUser> _signInManager;
         private readonly IEmailService _emailSender;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }


        public async Task<IdentityResult> RegisterAsync(UserRegister model)
         {

             var user = new ApplicationUser
             {
                 FirstName = model.FirstName,
                 LastName = model.LastName,
                 Email = model.Email,
                 UserName = model.Email,
                 PhoneNumber = model.PhoneNumber,
                 Address = model.Address,
                 CreatedAt = DateTime.Now,
             };


            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user,"Customer");
                //make sign in user dreict after create account
                await _signInManager.SignInAsync(user, false);

            }


            return result;

        }

        public async Task<SignInResult> LoginAsync(string email, string password,bool rememberMe)
         {
             var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
             return result;
         }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();

        public async Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(ApplicationUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return _userManager.Users.OrderByDescending(u => u.CreatedAt).ToList();
        }
        
        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user,string currentPass,string newPass)
        {
            return await _userManager.ChangePasswordAsync(user, currentPass, newPass);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return (List<string>)await _userManager.GetRolesAsync(user);
        }

        public (IQueryable<ApplicationUser>,int) PaginateUsers(int page, int pageSize)
        {
            var query = _userManager.Users.OrderByDescending(p => p.CreatedAt);

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

            var paginated = query.Paginate(page,pageSize);

            return (paginated, totalPages);
        }

        public async Task<bool> IsEmailExists(string email)
        {
             var isExsit = await _userManager.FindByEmailAsync(email);
             return (isExsit != null) ?   true : false;

        }

        //Reset Password Create link reset wiht token

        public async Task<bool> SendPasswordResetLink(string email , string baseUrl)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var getToken = await _userManager.GeneratePasswordResetTokenAsync(user);


            var link = $"{baseUrl}/Account/ResetPassword?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(getToken)}";


            var body = $@"
        <h3>Hello {user.FirstName}</h3>
        <p>Click the button below to reset your password:</p>
        <a href='{link}' 
           style='display:inline-block;
                  padding:12px 24px;
                  background:#007bff;
                  color:#fff;
                  text-decoration:none;
                  border-radius:6px;'>
            Reset Password
        </a>
        <p style='margin-top:15px;font-size:12px;color:#666;'>
            If you didn't request this, you can ignore this email.
        </p>";

           
            
            await _emailSender.SendEmailAsync(user.FirstName, user.Email!,
                      "Reset Your Password", body);


            return true;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return null;

            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            return result;
        }




    }



}


