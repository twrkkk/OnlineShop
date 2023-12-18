using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OnlineShop.Db;
using OnlineShop.Db.Enums;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Interfaces;

namespace OnlineShopWebApp.Storages
{
    public class DbOrderStorage : IOrderStorage
    {
        private readonly DatabaseContext _databaseContext;

        public DbOrderStorage(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Add(Order orderInfo)
        {
            _databaseContext.Orders.Add(orderInfo);

            _databaseContext.SaveChanges();
        }

        public Order TryGetByOrderId(Guid orderId)
        {
            return GetOrdersQuery().FirstOrDefault(x => x.Id == orderId);
        }

        public List<Order> GetOrders()
        {
            return GetOrdersQuery().ToList();
        }

        public void UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            var order = TryGetByOrderId(orderId);
            if (order != null)
            {
                order.OrderStatus = status;
            }

            _databaseContext.SaveChanges();
        }

        public void UpdatePaymentStatus(Guid orderId, PaymentStatus status)
        {
            var order = TryGetByOrderId(orderId);
            if (order != null)
            {
                order.PaymentStatus = status;
            }

            _databaseContext.SaveChanges();
        }

        public IIncludableQueryable<Order, Product> GetOrdersQuery()
        {
            return _databaseContext.Orders.Include(x => x.OrderForm)
                .Include(x => x.CartProducts).ThenInclude(x => x.Product);
        }

        public void UpdateStatusList(IQueryable<Order> orders, OrderStatus status)
        {
            foreach (var order in orders)
            {
                order.OrderStatus = status;
            }

            _databaseContext.SaveChanges();
        }
    }
}
