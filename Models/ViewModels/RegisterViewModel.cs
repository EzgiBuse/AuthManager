using System.ComponentModel.DataAnnotations;

namespace AuthManager.Models.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The password and confirmation does not match")]
        public string ConfirmPassword { get; set; }

       
    }
}
