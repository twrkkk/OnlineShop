using Microsoft.AspNetCore.Identity;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using User = OnlineShop.Db.Models.User;

namespace OnlineShopWebApp.Storages
{

    public class DbUsersStorage : IUsersStorage
    {
        private readonly UserManager<User> _userManager;

        public DbUsersStorage(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public void Add(string email, string password, string role = Constants.UserRoleName)
        {
            var newUser = new User { Email = email, UserName = email };
            var result = _userManager.CreateAsync(newUser, password).Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(newUser, role).Wait();
            }
        }

        public void Remove(User user)
        {
            _userManager.DeleteAsync(user).Wait();
        }

        public List<User>? GetPartOfUsers(int page, int count = 10)
        {
            return _userManager.Users.Skip((page - 1) * count).Take(count).ToList();
        }

        public User? TryGetByEmail(string userEmail)
        {
            return _userManager.FindByEmailAsync(userEmail).Result;
        }

        public IdentityResult ChangePassword(User user, string newPassword)
        {
            _userManager.RemovePasswordAsync(user).Wait();
            var result = _userManager.AddPasswordAsync(user, newPassword).Result;

            return result;
        }

        public IQueryable<User> GetAll()
        {
            return _userManager.Users;
        }

        public void Update(User user)
        {
            _userManager.UpdateAsync(user).Wait();  
        }

        public int GetCount()
        {
            return _userManager.Users.Count();
        }
    }
}
