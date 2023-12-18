using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;
using System;
using User = OnlineShop.Db.Models.User;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartStorage _cartStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly UserManager<User> _userManager;

        public OrderController(ICartStorage cartStorage, IOrderStorage orderStorage, UserManager<User> userManager)
        {
            _cartStorage = cartStorage;
            _orderStorage = orderStorage;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderCheck(OrderFormViewModel form)
        {
            if (ModelState.IsValid)
            {
                var _userId = new Guid(_userManager.GetUserId(User));
                var userCart = _cartStorage.TryGetByUserId(_userId);

                var orderInfo = new Order
                {
                    CartProducts = userCart?.CartProducts,
                    OrderForm = Mapping.ToDbModel(form),
                    UserId = userCart.UserId
                };

                _orderStorage.Add(orderInfo);

                _cartStorage.RemoveCart(_userId);
                return RedirectToAction("SuccessfulOrder");
            }

            return View("Index");
        }

        public IActionResult SuccessfulOrder()
        {
            return View();
        }
    }
}
