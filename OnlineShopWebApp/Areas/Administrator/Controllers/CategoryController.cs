using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Storages;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Data;
using System.Linq;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class CategoryController : Controller
    {
        private readonly ICategoryStorage _categoryStorage;
        private readonly IProductStorage _productStorage;

        public CategoryController(ICategoryStorage categoryStorage, IProductStorage productStorage)
        {
            _categoryStorage = categoryStorage;
            _productStorage = productStorage;
        }

        public IActionResult Index()
        {
            var categories = _categoryStorage.GetAll().ToList();
            return View(Mapping.ToViewModels(categories));
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Edit(string categoryName)
        {
            var category = _categoryStorage.TryGetByName(categoryName);
            return View(category);
        }

        [HttpPost]
        public IActionResult Add(CategoryViewModel category)
        {
            if (!ModelState.IsValid)
                return View(category);

            var existCategory = _categoryStorage.TryGetByName(category.Name);

            if (existCategory != null)
                ModelState.AddModelError("", "Такая категория уже существует");

            if (!ModelState.IsValid)
                return View(category);

            var dbCategory = Mapping.ToDbModel(category);
            _categoryStorage.Add(dbCategory);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string categoryName)
        {
            var category = _categoryStorage.GetAllWithProducts().FirstOrDefault(x => x.Name == categoryName);

            if (category != null)
            {
                if(category.Products != null)
                {
                    var products = category.Products.ToList();
                    foreach (var product in products) 
                    {
                        _productStorage.DeleteProduct(product);
                    }
                }

                _categoryStorage.Delete(category);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel category)
        {
            if (!ModelState.IsValid)
                return View(category);

            var dbCategory = Mapping.ToDbModel(category);
            _categoryStorage.Update(dbCategory);

            return RedirectToAction("Index");
        }

        public IActionResult AddProductCategory(string categoryName, Guid productId)
        {
            var product = _productStorage.TryGetById(productId);
            if (product == null)
                return RedirectToAction("Index", "Product");

            var category = _categoryStorage.TryGetByName(categoryName);
            if (category == null)
                return RedirectToAction("Index", "Category");

            product.Categories.Add(category);
            _productStorage.UpdateProduct(product);

            return RedirectToAction("Edit", "Product", new { productId = productId });
        }

        public IActionResult DeleteProductCategory(string categoryName, Guid productId)
        {
            var product = _productStorage.TryGetById(productId);
            if (product == null)
                return RedirectToAction("Index", "Product");

            var category = _categoryStorage.TryGetByName(categoryName);
            if (category == null)
                return RedirectToAction("Index", "Category");

            product.Categories.Remove(category);
            _productStorage.UpdateProduct(product);

            return RedirectToAction("Edit", "Product", new { productId = productId });
        }
    }
}
