using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MStore.Models
{
    public class UserProfileVm
    {

        [Required(ErrorMessage = "The First Name field is required"), MaxLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required"), MaxLength(100)]
        public string LastName { get; set; }

        [Required , MaxLength(200)]
        [EmailAddress(ErrorMessage = "Email syntax not allowed")]
        [Remote("CheckEmailExists","Account",ErrorMessage = "email is already used !")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "The format of the Phone Number is not valid"), MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required, MaxLength(200)]
        public string Address { get; set; } = "";

        public DateTime CreatedAt { get; set; }
    
    }
}
