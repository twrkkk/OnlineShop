using OnlineShopWebApp.Areas.Administrator.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class RegistrationForm
    {
        public RegistrationForm() { }
        public RegistrationForm(EditProfile profile) 
        {
            Email = profile.Email;
            Phone = profile.Phone;
            Age = profile.Age;
        }

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Введите правильный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"^((\+7|7|8)+([0-9]){10})$", ErrorMessage = "Неверный формат телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите возраст")]
        [Range(18, 100, ErrorMessage = "Возраст должен быть от 18 до 100")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(40, ErrorMessage = "Длина пароля от 6 до 40 символов", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите пароль еще раз")]
        [Compare("Password", ErrorMessage = "Введенные пароли не совпадают")]
        [StringLength(40, ErrorMessage = "Длина пароля от 6 до 40 символов", MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }

        public override bool Equals(object obj)
        {
            return obj is RegistrationForm form &&
                   (Email == form.Email ||
                   Phone == form.Phone);
        }
    }
}
