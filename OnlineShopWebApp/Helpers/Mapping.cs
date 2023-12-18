using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Enums;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using User = OnlineShop.Db.Models.User;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static List<CartProductViewModel> ToViewModel(List<CartProduct> carts)
        {
            return carts?.Select(x => ToViewModel(x)).ToList();
        }

        public static CartProductViewModel ToViewModel(CartProduct cartItem)
        {
            if (cartItem == null) return null;
            return new CartProductViewModel { Id = cartItem.Id, Product = cartItem.Product, Count = cartItem.Count };
        }

        public static CartViewModel ToViewModel(Cart cart)
        {
            if (cart == null) return null;

            var cartViewModel = new CartViewModel
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartProducts = ToViewModel(cart.CartProducts)
            };

            return cartViewModel;
        }

        public static List<ProductViewModel> ToViewModel(List<Product> items)
        {
            return items?.Select(x => ToViewModel(x)).ToList();
        }

        public static ProductViewModel ToViewModel(Product item)
        {
            if (item == null) return null;

            return new ProductViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Cost = item.Cost,
                IconSource = item.IconSource,
                Categories = ToViewModels(item.Categories)
            };
        }

        public static List<OrderViewModel> ToViewModel(List<Order> orders)
        {
            return orders?.Select(x => ToViewModel(x)).ToList();
        }

        public static OrderViewModel ToViewModel(Order order)
        {
            return new OrderViewModel
            {
                Products = order.CartProducts.Select(x => ToViewModel(x)).ToList(),
                OrderTime = order.OrderTime,
                OrderForm = ToViewModel(order.OrderForm),
                OrderId = order.Id,
                UserId = order.UserId,
                OrderStatus = (OrderStatus)(int)order.OrderStatus,
                PaymentStatus = (PaymentStatus)(int)order.PaymentStatus
            };
        }

        public static OrderFormViewModel ToViewModel(OrderForm form)
        {
            if (form == null) return null;

            return new OrderFormViewModel
            {
                Name = form.Name,
                Surname = form.Surname,
                Phone = form.Phone,
                Address = form.Address
            };
        }

        public static Order ToDbModel(OrderViewModel formViewModel)
        {
            if (formViewModel == null) return null;

            return new Order
            {
                Id = formViewModel.OrderId,
                UserId = formViewModel.UserId,
                OrderForm = ToDbModel(formViewModel.OrderForm),
                OrderTime = formViewModel.OrderTime
            };
        }

        public static OrderForm ToDbModel(OrderFormViewModel orderFormViewModel)
        {
            if (orderFormViewModel == null) return null;

            return new OrderForm
            {
                Name = orderFormViewModel.Name,
                Surname = orderFormViewModel.Surname,
                Phone = orderFormViewModel.Phone,
                Address = orderFormViewModel.Address
            };
        }

        public static List<UserViewModel> ToViewModel(List<User> users)
        {
            return users?.Select(x => ToViewModel(x)).ToList();
        }

        public static UserViewModel ToViewModel(User user)
        {
            var info = new RegistrationForm { Email = user.Email, Phone = user.PhoneNumber };
            return new UserViewModel { Id = new Guid(user.Id), RegistrationInfo = info, Name = user.Name, Surname = user.Surname };
        }

        public static RoleViewModel ToViewModel(UserRole role)
        {
            return new RoleViewModel
            {
                Name = role.Name,
            };
        }

        public static UserRole ToDbModel(RoleViewModel roleViewModel)
        {
            return new UserRole
            {
                Name = roleViewModel.Name,
            };
        }

        public static List<RoleViewModel> ToViewModels(List<UserRole> roles)
        {
            return roles.Select(x => ToViewModel(x)).ToList();
        }

        public static List<CategoryViewModel> ToViewModels(List<Category> categories)
        {
            return categories.Select(x => ToViewModel(x)).ToList();
        }

        public static CategoryViewModel ToViewModel(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                Products = category.Products
            };
        }

        public static Category ToDbModel(CategoryViewModel categoryViewModel)
        {
            return new Category
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name,
            };
        }
    }
}
