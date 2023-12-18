using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IUsersStorage
    {
        void Add(string email, string password, string role = Constants.UserRoleName);
        IdentityResult ChangePassword(User user, string newPassword);
        IQueryable<User> GetAll();
        int GetCount();
        List<User>? GetPartOfUsers(int page, int count = 10);
        void Remove(User user);
        User? TryGetByEmail(string userEmail);
        void Update(User user);
    }
}