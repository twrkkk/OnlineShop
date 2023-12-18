using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Enums;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Interfaces;
using System;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrderStorage _orderStorage;

        public OrderController(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }

        public IActionResult Index()
        {
            var orders = _orderStorage.GetOrders();
            return View(Mapping.ToViewModel(orders));
        }

        public IActionResult ShowDetails(Guid orderId)
        {
            var order = _orderStorage.TryGetByOrderId(orderId);
            
            return View(Mapping.ToViewModel(order));
        }

        [HttpPost]
        public IActionResult EditOrder(Guid orderId, OrderStatus orderStatus, PaymentStatus paymentStatus)
        {
            _orderStorage.UpdateOrderStatus(orderId, orderStatus);
            _orderStorage.UpdatePaymentStatus(orderId, paymentStatus);
            return RedirectToAction("Index");
        }
    }
}
