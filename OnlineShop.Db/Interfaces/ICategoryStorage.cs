using Microsoft.EntityFrameworkCore.Query;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Storages
{
    public interface ICategoryStorage
    {
        void Add(Category category);
        void Delete(Category category);
        IQueryable<Category> GetAll();
        IIncludableQueryable<Category, List<Product>> GetAllWithProducts();
        Category? TryGetByName(string categoryName);
        void Update(Category category);
    }
}