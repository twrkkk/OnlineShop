using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class RoleViewModel
    {
        public RoleViewModel() { }

        [Required(ErrorMessage = "Введите название роли")]
        [StringLength(100, ErrorMessage = "Длина от 1 до 100 символов", MinimumLength = 1)]
        public string Name { get; set; }    
    }
}
