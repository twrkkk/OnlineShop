using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Storages
{
    public class DbProductStorage : IProductStorage
    {
        private readonly DatabaseContext _databaseContext;

        public DbProductStorage(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Product> GetProducts()
        {
            return _databaseContext.Products.Include(x=>x.Categories).ToList();
        }

        public Product TryGetById(Guid productId)
        {
            return _databaseContext.Products.Include(x=>x.Categories).FirstOrDefault(x => x.Id == productId);
        }

        public bool DeleteProduct(Guid productId)
        {
            var item = _databaseContext.Products.FirstOrDefault(x => x.Id == productId);

            if (item == null)
                return false;

            _databaseContext.Products.Remove(item);
            _databaseContext.SaveChanges();
            return true;
        }

        public void DeleteProduct(Product product)
        {
            _databaseContext.Products.Remove(product);
            _databaseContext.SaveChanges();
        }

        public void AddProduct(Product product)
        {
            if (TryGetById(product.Id) != null)
                return;

            _databaseContext.Products.Add(product);
            _databaseContext.SaveChanges();
        }

        public void UpdateProduct(Product product) 
        {
            _databaseContext.Products.Update(product);
            _databaseContext.SaveChanges();
        }
    }
}
