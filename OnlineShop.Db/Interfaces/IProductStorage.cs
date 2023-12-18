using OnlineShop.Db.Models;
using System.Collections.Generic;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductStorage
    {
        void AddProduct(Product item);
        bool DeleteProduct(Guid productId);
        void DeleteProduct(Product product);
        List<Product> GetProducts();
        Product TryGetById(Guid id);
        void UpdateProduct(Product product);
    }
}
