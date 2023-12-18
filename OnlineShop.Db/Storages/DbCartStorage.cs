using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Storages
{
    public class DbCartStorage : ICartStorage
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IProductStorage _productStorage;

        public DbCartStorage(DatabaseContext databaseContext, IProductStorage productStorage)
        {
            _databaseContext = databaseContext;
            _productStorage = productStorage;
        }

        public Cart TryGetByUserId(Guid userId)
        {
            return _databaseContext.Carts.Include(x=>x.CartProducts).ThenInclude(x=>x.Product).FirstOrDefault(x => x.UserId == userId);
        }

        public Cart CreateNewCart(Guid userId)
        {
            var cart = new Cart();
            cart.Id = userId;
            _databaseContext.Carts.Add(cart);
            _databaseContext.SaveChanges();

            return cart;
        }

        public bool RemoveCart(Guid userId)
        {
            Cart cart = TryGetByUserId(userId);
            if (cart != null)
            {
                _databaseContext.Carts.Remove(cart);
                _databaseContext.SaveChanges();
                return true;
            }

            return false;
        }

        public void DeleteItemFromEveryoneCarts(Guid productId)
        {
            foreach (var cart in _databaseContext.Carts)
            {
                var cartItem = cart.CartProducts?.FirstOrDefault(x => x.Id == productId);

                if (cartItem == null)
                    continue;

                cart.CartProducts?.Remove(cartItem);
            }

            _databaseContext.SaveChanges();
        }

        public void Add(Guid productId, Guid userId, int count = 1)
        {
            var product = _productStorage.TryGetById(productId);
            if (product == null) return;

            var userCart = TryGetByUserId(userId);

            if (userCart == null)
            {
                userCart = new Cart
                {
                    UserId = userId
                };

                userCart.CartProducts = new List<CartProduct>
                {
                    new CartProduct
                    {
                        Product = product,
                        //Cart = userCart,
                        Count = count
                    }
                };

                _databaseContext.Carts.Add(userCart);
            }
            else
            {
                var cartItem = userCart.CartProducts?.FirstOrDefault(x => x.Product.Id == productId);
                //the product is already in the cart 
                if (cartItem != null)
                {
                    cartItem.Count += count;
                }
                else
                {
                    userCart?.CartProducts?.Add(new CartProduct
                    {
                        Product = product,
                        //Cart = userCart,
                        Count = count
                    });
                }
            }

            _databaseContext.SaveChanges();
        }

        public void Reduce(Guid productId, Guid userId, int count = 1)
        {
            if (count <= 0) return;

            var product = _productStorage.TryGetById(productId);
            if (product == null) return;

            var userCart = TryGetByUserId(userId);
            if (userCart == null) return;

            var cartItem = userCart.CartProducts?.FirstOrDefault(x => x.Product.Id == productId);
            if (cartItem == null) return;

            cartItem.Count -= count;
            if (cartItem.Count <= 0)
                userCart.CartProducts.Remove(cartItem);

            _databaseContext.SaveChanges();
        }

        public void Clear(Guid userId)
        {
            var userCart = TryGetByUserId(userId);
            if (userCart == null) return;

            userCart.CartProducts.Clear();

            _databaseContext.SaveChanges();
        }

        public void DeleteItem(Guid productId, Guid userId)
        {
            var userCart = TryGetByUserId(userId);
            if (userCart == null) return;

            var cartItem = userCart.CartProducts?.FirstOrDefault(x => x.Id == productId);
            userCart.CartProducts.Remove(cartItem);

            _databaseContext.SaveChanges();
        }

        public decimal TotalPrice(Guid userId)
        {
            var userCart = TryGetByUserId(userId);
            if (userCart == null) return 0;

            return userCart.CartProducts?.Sum(x => x.Count * x.Product.Cost) ?? 0;
        }

        public int Amount(Guid userId)
        {
            var userCart = TryGetByUserId(userId);
            if (userCart == null) return 0;

            return userCart.CartProducts?.Sum(x => x.Count) ?? 0;
        }
    }
}
