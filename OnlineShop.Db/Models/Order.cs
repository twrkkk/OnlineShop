using OnlineShop.Db.Enums;

namespace OnlineShop.Db.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public OrderForm OrderForm { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime OrderTime { get; set; }

        public Order()
        {
            OrderStatus = OrderStatus.Created;
            PaymentStatus = PaymentStatus.NotPaid;
            OrderTime = DateTime.Now;
        }
            
    }
}
