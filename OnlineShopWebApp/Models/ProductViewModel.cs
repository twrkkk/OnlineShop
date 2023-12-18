using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Введите название")]
        [StringLength(100, ErrorMessage = "Длина от 1 до 100 сомволов", MinimumLength = 1)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите стоимость")]
        [Range(1, 10000, ErrorMessage = "Стоимость должна быть от 1 до 10000")]
        public decimal Cost { get; set; }
        [Required(ErrorMessage = "Введите описание")]
        [StringLength(300, ErrorMessage = "Длина от 1 до 300 сомволов", MinimumLength = 1)]
        public string Description { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
        public string IconSource { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
