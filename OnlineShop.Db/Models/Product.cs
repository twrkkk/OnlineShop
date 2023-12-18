using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string IconSource { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public List<Category> Categories { get; set; }

        public Product()
        {
            CartProducts = new();
            Categories = new();
        }

        public override bool Equals(object? obj)
        {
            return obj is Product product&& 
                product.Id == Id;
        }
    }
}
