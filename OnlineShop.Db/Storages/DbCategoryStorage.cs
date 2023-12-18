using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OnlineShop.Db.Models;
using System.Security.Cryptography.X509Certificates;

namespace OnlineShop.Db.Storages
{
    public class DbCategoryStorage : ICategoryStorage
    {
        private readonly DatabaseContext _databaseContext;

        public DbCategoryStorage(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(Category category)
        {
            _databaseContext.Categories.Add(category);
            _databaseContext.SaveChanges();
        }

        public void Delete(Category category)
        {
            _databaseContext.Categories.Remove(category);
            _databaseContext.SaveChanges();
        }

        public void Update(Category category)
        {
            _databaseContext.Categories.Update(category);
            _databaseContext.SaveChanges();
        }

        public Category? TryGetByName(string categoryName)
        {
            return _databaseContext.Categories.FirstOrDefault(x => x.Name == categoryName);
        }

        public IIncludableQueryable<Category, List<Product>> GetAllWithProducts() 
        {
            return _databaseContext.Categories.Include(x=>x.Products);
        }

        public IQueryable<Category> GetAll()
        {
            return _databaseContext.Categories;
        }
    }
}
