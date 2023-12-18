using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShopWebApp.Helpers;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductStorage _productStorage;

        public ProductController(IProductStorage productStorage)
        {
            _productStorage = productStorage;
        }

        public IActionResult Index(Guid productId)
        {
            var item = _productStorage.TryGetById(productId);
            return View(Mapping.ToViewModel(item));
        }
    }
}
