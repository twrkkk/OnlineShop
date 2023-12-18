using OnlineShopWebApp.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Areas.Administrator.Models
{
    public class EditProfile
    {
        public EditProfile() { }
        public EditProfile(RegistrationForm info)
        {
            Email = info.Email;
            Phone = info.Phone;
            Age = info.Age;
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

        public string Name { get;set; }
        public string Surname { get;set; }

        public override bool Equals(object obj)
        {
            return obj is EditProfile profile &&
                   (Email == profile.Email ||
                   Phone == profile.Phone);
        }
    }
}
