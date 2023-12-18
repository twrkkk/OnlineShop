namespace OnlineShop.Db.Models
{
    public class CartProduct
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }

    }
}
