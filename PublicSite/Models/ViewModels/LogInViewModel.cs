using System.ComponentModel.DataAnnotations;

namespace PublicSite.Models.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "E-mail is required")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}