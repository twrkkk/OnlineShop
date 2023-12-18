using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using User = OnlineShop.Db.Models.User;

namespace OnlineShopWebApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly SignInManager<User> _signInManager;

        public AuthorizationController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index(string returnUrl)
        {
            return View(new AuthorizationForm { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult AuthorizationCheck(AuthorizationForm form)
        {
            if (!ModelState.IsValid)
                return View("Index", form);

            var result = _signInManager.PasswordSignInAsync(form.Email, form.Password, form.RememberMe, false).Result;
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(form.ReturnUrl) && Url.IsLocalUrl(form.ReturnUrl))
                    return Redirect(form.ReturnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Неверный логин или пароль");
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
    }
}
