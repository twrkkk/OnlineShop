using OnlineShop.Db.Models;
using System;

namespace OnlineShopWebApp.Models
{
    public class CartProductViewModel
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
        public int Count { get; set; }
    }
}
