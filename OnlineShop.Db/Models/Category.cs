using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
