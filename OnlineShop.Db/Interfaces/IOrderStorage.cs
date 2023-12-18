using Microsoft.EntityFrameworkCore.Query;
using OnlineShop.Db.Enums;
using OnlineShop.Db.Models;

namespace OnlineShopWebApp.Interfaces
{
    public interface IOrderStorage
    {
        void Add(Order orderInfo);
        List<Order> GetOrders();
        IIncludableQueryable<Order, Product> GetOrdersQuery();
        Order TryGetByOrderId(Guid userId);
        void UpdateOrderStatus(Guid orderId, OrderStatus status);
        void UpdatePaymentStatus(Guid orderId, PaymentStatus status);
        void UpdateStatusList(IQueryable<Order> orders, OrderStatus status);
    }
}