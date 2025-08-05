using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MStore.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The First Name field is required"), MaxLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required"), MaxLength(100)]
        public string LastName { get; set; }

        [Required,MaxLength(100)]
        [EmailAddress(ErrorMessage = "Email syntax not allowed")]
        [Remote(action: "CheckEmailExists", controller: "Account", ErrorMessage = "email is already used !")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "The format of the Phone Number is not valid"), MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required, MaxLength(200)]
        public string Address { get; set; } = "";

        [Required, MaxLength(100)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Confirm Password field is required")]
        [Compare("Password", ErrorMessage = "Confirm Password and Password do not match")]
        public string ConfirmPassword { get; set; } = "";
    }
}
