using System.ComponentModel.DataAnnotations;

namespace MStore.Models
{
    public class UpdatePasswordVm
    {

        [Required(ErrorMessage = "The Password field is required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "The NewPassword field is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "The Confirm Password field is required")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password and NewPassword do not match")]
        public string ConfirmPassword { get; set; }


    }

}
