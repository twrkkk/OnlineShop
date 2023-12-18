using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartStorage
    {
        bool RemoveCart(Guid userId);
        void DeleteItemFromEveryoneCarts(Guid productId);
        Cart TryGetByUserId(Guid id);
        Cart CreateNewCart(Guid userId);
        void Add(Guid productId, Guid userId, int count = 1);
        void Reduce(Guid productId, Guid userId, int count = 1);
        void Clear(Guid userId);
        void DeleteItem(Guid productId, Guid userId);
        decimal TotalPrice(Guid userId);
        int Amount(Guid userId);
    }
}