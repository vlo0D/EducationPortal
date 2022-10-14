using System.ComponentModel.DataAnnotations;

namespace EducationPortal.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login can not be Empty")]
        [Display(Name="Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password can not be Empty")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
