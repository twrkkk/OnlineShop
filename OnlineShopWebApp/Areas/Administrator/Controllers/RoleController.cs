using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;
using System.Linq;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RoleController : Controller
    {
        private readonly IRolesStorage _rolesStorage;
        private readonly UserManager<User> _userManager;
        public RoleController(IRolesStorage rolesStorage, UserManager<User> userManager)
        {
            _rolesStorage = rolesStorage;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _rolesStorage.GetAll().ToList();
            return View(Mapping.ToViewModels(roles));
        }

        public IActionResult Delete(string name)
        {
            var role = _rolesStorage.TryGetByName(name);
            if (role == null)
                return RedirectToAction("Index");

            var usersInRole = _userManager.GetUsersInRoleAsync(name).Result;
            foreach (var user in usersInRole)
            {
                _userManager.RemoveFromRoleAsync(user, name).Wait();
            }

            _rolesStorage.Remove(name);

            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RoleViewModel roleViewModel)
        {
            if (_rolesStorage.Contains(roleViewModel.Name))
                ModelState.AddModelError("", "Такая роль уже существует");

            if (!ModelState.IsValid)
                return View(roleViewModel);

            var dbRole = Mapping.ToDbModel(roleViewModel);
            _rolesStorage.Add(dbRole);

            return RedirectToAction("Index");
        }
    }
}
