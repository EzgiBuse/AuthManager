using System.ComponentModel.DataAnnotations;

namespace AuthManager.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
       
    }
}
