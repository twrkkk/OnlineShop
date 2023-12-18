using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Administrator.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;
using System;
using System.Linq;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly IUsersStorage _usersStorage;
        private readonly IRolesStorage _rolesStorage;
        private readonly UserManager<User> _userManager;

        public UserController(IUsersStorage usersStorage, IRolesStorage rolesStorage, UserManager<User> userManager)
        {
            _usersStorage = usersStorage;
            _rolesStorage = rolesStorage;
            _userManager = userManager;
        }

        public IActionResult Index(int page = 1, int pageSize = 3)
        {
            var usersCount = _usersStorage.GetCount();
            var users = _usersStorage.GetPartOfUsers(page, pageSize);
            var userViewModels = Mapping.ToViewModel(users);
            var pageViewModel = new PageViewModel(usersCount, page, pageSize);
            var viewModel = new IndexViewModel<UserViewModel>
            {
                PageViewModel = pageViewModel,
                Entities = userViewModels
            };
            return View(viewModel);
        }

        public IActionResult Show(string userEmail)
        {
            var user = _usersStorage.TryGetByEmail(userEmail);
            if (user != null)
            {
                var userRoles = _userManager.GetRolesAsync(user).Result;
                ViewData["Roles"] = userRoles;
            }
            return View(Mapping.ToViewModel(user));
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult ChangePassword(string userEmail)
        {
            var changePassword = new NewPassword()
            {
                Email = userEmail
            };

            return View(changePassword);
        }

        public IActionResult EditProfile(string userEmail)
        {
            var user = _usersStorage.TryGetByEmail(userEmail);

            if (user == null)
                return RedirectToAction("Index");

            var profile = new EditProfile
            {
                Email = userEmail,
            };

            return View(profile);
        }

        public IActionResult Delete(string userEmail)
        {
            var user = _usersStorage.TryGetByEmail(userEmail);

            if (user == null)
                return RedirectToAction("Index");

            _usersStorage.Remove(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(RegistrationForm form)
        {
            var existUser = _usersStorage.TryGetByEmail(form.Email);

            if (existUser != null)
                ModelState.AddModelError("", "Такой пользователь уже существует");

            if (!ModelState.IsValid)
                return View(form);

            _usersStorage.Add(form.Email, form.Password);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangePassword(NewPassword form)
        {
            if (!ModelState.IsValid)
                return View(form);

            var user = _usersStorage.TryGetByEmail(form.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Такого пользователя не существует");
            }

            if (!ModelState.IsValid)
                return View(form);

            var result = _usersStorage.ChangePassword(user, form.Password);
            if (result.Succeeded) //change succesful
            {
                return RedirectToAction("Show", new { userEmail = form.Email });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(form);
        }

        [HttpPost]
        public IActionResult EditProfile(EditProfile newProfile)
        {
            if (!ModelState.IsValid)
                return View(newProfile);

            var existUser = _usersStorage.GetAll().FirstOrDefault(x =>
                (x.Email != newProfile.Email &&
                x.PhoneNumber == newProfile.Phone)
            );

            if (existUser != null) // мы ввели чужие данные (номер телефона)
                ModelState.AddModelError("", "Такой пользователь уже существует");

            if (!ModelState.IsValid)
                return View(newProfile);

            var user = _usersStorage.TryGetByEmail(newProfile.Email);

            user.Email = newProfile.Email;
            user.UserName = newProfile.Email;
            user.PhoneNumber = newProfile.Phone;
            user.Name = newProfile.Name;
            user.Surname = newProfile.Surname;

            _usersStorage.Update(user);

            return RedirectToAction("Show", new { userEmail = user.Email });
        }

        public IActionResult SetUserRole(string userEmail)
        {
            var roles = Mapping.ToViewModels(_rolesStorage.GetAll().ToList());
            ViewData["UserEmail"] = userEmail;
            var selectList = new SelectList(roles, "Name", "Name");
            return View(selectList);
        }

        [HttpPost]
        public IActionResult SetUserRole(string userEmail, string roleName)
        {
            var user = _usersStorage.TryGetByEmail(userEmail);
            if (user == null)
                return RedirectToAction("Show", new { userEmail = userEmail });

            if (_userManager.IsInRoleAsync(user, roleName).Result)
                return RedirectToAction("Show", new { userEmail = userEmail });

            var userRoles = _userManager.GetRolesAsync(user).Result;
            var result = _userManager.RemoveFromRolesAsync(user, userRoles).Result;

            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(user, roleName).Wait();
            }

            return RedirectToAction("Show", new { userEmail = userEmail });
        }
    }
}
