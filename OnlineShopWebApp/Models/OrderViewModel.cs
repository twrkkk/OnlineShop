using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShopWebApp.Enums;

namespace OnlineShopWebApp.Models
{
    public class OrderViewModel
    {
        public List<CartProductViewModel> Products { get; set; }  
        public DateTime OrderTime { get; set; }
        public OrderFormViewModel OrderForm { get; set; }
        public Guid OrderId { get; set; }   
        public Guid UserId { get; set; }   
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public decimal TotalPrice => Products.Sum(x => x.Count * x.Product.Cost);

        public OrderViewModel() { }
    }
}
