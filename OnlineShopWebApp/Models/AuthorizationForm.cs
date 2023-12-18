using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class AuthorizationForm
    {
        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите правильный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(40, ErrorMessage = "Длина пароля от 6 до 40 символов", MinimumLength = 6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }   
        
        public string ReturnUrl { get; set; }
    }
}
