using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using System.Linq;
using User = OnlineShop.Db.Models.User;

namespace OnlineShopWebApp.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public RegistrationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new RegistrationForm { ReturnUrl = returnUrl});
        }

        [HttpPost]
        public IActionResult RegistrationCheck(RegistrationForm form)
        {
            if(!ModelState.IsValid) 
                return View("Index", form);

            var existUser = _userManager.FindByEmailAsync(form.Email).Result;
            if (existUser != null)
                ModelState.AddModelError("", "Пользователь с таким email уже существует");

            existUser = _userManager.Users.FirstOrDefault(x => x.PhoneNumber == form.Phone);
            if (existUser != null)
                ModelState.AddModelError("", "Пользователь с таким номером телефона уже существует");

            if (!ModelState.IsValid)
                return View("Index", form);

            var user = new User { Email = form.Email, UserName = form.Email, PhoneNumber = form.Phone, Age = form.Age };
            var result = _userManager.CreateAsync(user, form.Password).Result;

            if (result.Succeeded)
            {
                _signInManager.SignInAsync(user, false).Wait();

                if (!string.IsNullOrEmpty(form.ReturnUrl) && Url.IsLocalUrl(form.ReturnUrl))
                    return Redirect(form.ReturnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("Index", form);
        }
    }
}
