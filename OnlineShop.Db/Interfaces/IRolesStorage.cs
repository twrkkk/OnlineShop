using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Interfaces
{
    public interface IRolesStorage
    {
        void Add(UserRole role);
        bool Contains(string roleName);
        IQueryable<UserRole> GetAll();
        void Remove(string roleName);
        UserRole? TryGetByName(string roleName);
    }
}