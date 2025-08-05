using ECommerceStore.BLL.DTOs;
using ECommerceStore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ECommerceStore.BLL
{
   
        public interface IUserService
        {
            Task<IdentityResult> RegisterAsync(UserRegister model);
            Task<SignInResult> LoginAsync(string email, string password,bool rememberMe);
            Task LogoutAsync();
            Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal user);
            Task<IdentityResult> UpdateUserAsync(ApplicationUser user);
            Task<IdentityResult> DeleteUserAsync(ApplicationUser user);
            Task<List<ApplicationUser>> GetAllUsersAsync();
            Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPass, string newPass);
            Task<ApplicationUser> GetUserByIdAsync(string id);
            Task<List<string>> GetUserRoles(ApplicationUser user);
           (IQueryable<ApplicationUser>, int) PaginateUsers(int page, int pageSize);
           Task<bool> IsEmailExists(string email);


           Task<bool> SendPasswordResetLink(string email, string baseUrl);
           Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto dto);


    }







}
