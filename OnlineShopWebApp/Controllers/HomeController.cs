using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductStorage _productStorage;

        public HomeController(IProductStorage productStorage)
        {
            _productStorage = productStorage;
        }

        public IActionResult Index()
        {
            var products = Mapping.ToViewModel(_productStorage.GetProducts());

            //var groupedProductsByCategory = products
            //.SelectMany(p => p.Categories, (product, category) => new { Product = product, Category = category })
            //.GroupBy(pc => pc.Category)
            //.ToDictionary(group => group.Key.Name, group => group.Select(pc => pc.Product).ToList());

            string defaultCategory = "Без категории";
            var groupedProductsByCategory = new Dictionary<string, List<ProductViewModel>>();
            foreach (var product in products)
            {
                if (product.Categories != null && product.Categories.Count > 0)
                {
                    foreach (var category in product.Categories)
                    {
                        if (!groupedProductsByCategory.ContainsKey(category.Name))
                        {
                            groupedProductsByCategory[category.Name] = new List<ProductViewModel>();
                        }

                        groupedProductsByCategory[category.Name].Add(product);
                    }
                }
                else
                {
                    if (!groupedProductsByCategory.ContainsKey(defaultCategory))
                    {
                        groupedProductsByCategory[defaultCategory] = new List<ProductViewModel>();
                    }

                    groupedProductsByCategory[defaultCategory].Add(product);
                }
            }


            return View(groupedProductsByCategory);
        }

        [HttpPost]
        public IActionResult Search(string productName)
        {
            var products = _productStorage.GetProducts();
            var result = products.Where(x => x.Name.ToLower().Contains(productName.ToLower())).ToList();

            return View("Index", result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
