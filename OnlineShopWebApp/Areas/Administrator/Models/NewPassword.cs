using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Administrator.Models
{
    public class NewPassword
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(40, ErrorMessage = "Длина пароля от 6 до 40 символов", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите пароль еще раз")]
        [Compare("Password", ErrorMessage = "Введенные пароли не совпадают")]
        [StringLength(40, ErrorMessage = "Длина пароля от 6 до 40 символов", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}
