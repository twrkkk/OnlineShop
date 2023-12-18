using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using System;
using System.Security.Claims;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    [Authorize]
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartStorage _cartStorage;
        private readonly UserManager<User> _userManager;

        public CartViewComponent(ICartStorage cartStorage, UserManager<User> userManager)
        {
            _cartStorage = cartStorage;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            Guid _userId = Guid.Empty;
            if (User.Identity.IsAuthenticated)
            {
                _userId = new Guid(_userManager.GetUserId((ClaimsPrincipal)User));
            }
            var productCounts = _cartStorage.Amount(_userId);
            return View("Cart", productCounts);
        }
    }
}
