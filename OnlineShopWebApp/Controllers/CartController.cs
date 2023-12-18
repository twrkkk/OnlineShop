using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartStorage _cartStorage;
        private readonly UserManager<User> _userManager;

        public CartController(ICartStorage cartStorage, UserManager<User> userManager)
        {
            _cartStorage = cartStorage;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var _userId = new Guid(_userManager.GetUserId(User));
            var cart = _cartStorage.TryGetByUserId(_userId);

            return View(Mapping.ToViewModel(cart));
        }

        public IActionResult Add(Guid productId)
        {
            var _userId = new Guid(_userManager.GetUserId(User));
            _cartStorage.Add(productId, _userId);
            return RedirectToAction("Index");
        }

        public IActionResult Reduce(Guid productId)
        {
            var _userId = new Guid(_userManager.GetUserId(User));
            _cartStorage.Reduce(productId, _userId);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            var _userId = new Guid(_userManager.GetUserId(User));
            _cartStorage.Clear(_userId);
            return RedirectToAction("Index");
        }
        public IActionResult ClearPopup()
        {
            return PartialView("ModalClearCart");
        }

        public void DeleteItem(Guid productId)
        {
            var _userId = new Guid(_userManager.GetUserId(User));
            _cartStorage.DeleteItem(productId, _userId);
        }
    }
}
