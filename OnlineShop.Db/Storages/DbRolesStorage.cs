using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Interfaces;

namespace OnlineShop.Db.Storages
{
    public class DbRolesStorage : IRolesStorage
    {
        private readonly RoleManager<UserRole> _roleManager;

        public DbRolesStorage(RoleManager<UserRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void Add(UserRole role)
        {
            var existRole = TryGetByName(role.Name);
            if (existRole == null)
            {
                _roleManager.CreateAsync(role).Wait();
            }
        }

        public void Remove(string roleName)
        {
            var existRole = TryGetByName(roleName);
            if (existRole != null)
            {
                _roleManager.DeleteAsync(existRole);
            }
        }

        public IQueryable<UserRole> GetAll()
        {
            return _roleManager.Roles;
        }

        public UserRole? TryGetByName(string roleName)
        {
            return _roleManager.FindByNameAsync(roleName).Result;
        }

        public bool Contains(string roleName) 
        {
            return TryGetByName(roleName) != null;
        }
    }
}
