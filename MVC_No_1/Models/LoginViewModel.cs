using System.ComponentModel.DataAnnotations;

namespace MVC_No_1.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your username or email.")]
        [Display(Name = "Username/Email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
