using System.Collections.Generic;
using System;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public List<CartProductViewModel> CartProducts { get; set; }
        public Guid UserId { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return CartProducts.Sum(x => x.Count * x.Product.Cost);
            }
        }

        public int Amount
        {
            get
            {
                return CartProducts.Sum(x => x.Count);
            }
        }
    }
}
