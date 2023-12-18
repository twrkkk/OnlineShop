using OnlineShop.Db.Models;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
