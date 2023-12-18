using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop.Db.Storages;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IProductStorage _productStorage;
        private readonly ICartStorage _cartStorage;
        private readonly ICategoryStorage _categoryStorage;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductController(IProductStorage productStorage, ICartStorage cartStorage, IWebHostEnvironment appEnvironment, ICategoryStorage categoryStorage)
        {
            _productStorage = productStorage;
            _cartStorage = cartStorage;
            _appEnvironment = appEnvironment;
            _categoryStorage = categoryStorage;
        }

        public IActionResult Index()
        {
            var items = _productStorage.GetProducts();

            return View(Mapping.ToViewModel(items));
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(Guid productId)
        {
            var product = _productStorage.TryGetById(productId);
            if (product == null)
                return RedirectToAction("Index");

            var currentCategories = product.Categories;
            var availableCategories = _categoryStorage.GetAllWithProducts().Where(category => !currentCategories.Contains(category)).ToList();

            ViewData["CurrentCategories"] = Mapping.ToViewModels(currentCategories);
            ViewData["AvailableCategories"] = Mapping.ToViewModels(availableCategories);

            return View(Mapping.ToViewModel(product));
        }

        [HttpPost]
        public IActionResult Save(ProductViewModel item, Guid productId)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Edit", item);

            var product = _productStorage.TryGetById(productId);
            if (product == null)
                return RedirectToAction("Index");

            string productImagesPath = Path.Combine(_appEnvironment.WebRootPath + ImagePath.Products);
            var imageSaved = Image.Upload(item.UploadedFile, productImagesPath, out string fileName);

            product.Name = item.Name;
            product.Cost = item.Cost;
            product.Description = item.Description;
            product.IconSource = imageSaved ? ImagePath.Products + fileName : ImagePath.DefaultProduct;

            _productStorage.UpdateProduct(product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult New(ProductViewModel item)
        {
            if (!ModelState.IsValid)
                return View("Create", item);

            string productImagesPath = Path.Combine(_appEnvironment.WebRootPath + ImagePath.Products);
            var imageSaved = Image.Upload(item.UploadedFile, productImagesPath, out string fileName);

            var itemDb = new Product
            {
                Name = item.Name,
                Cost = item.Cost,
                Description = item.Description,
                IconSource = imageSaved ? ImagePath.Products + fileName : ImagePath.DefaultProduct
            };
            _productStorage.AddProduct(itemDb);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid productId)
        {
            _productStorage.DeleteProduct(productId);
            _cartStorage.DeleteItemFromEveryoneCarts(productId);

            return RedirectToAction("Index");
        }
    }
}
