namespace OnlineShop.Db.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public Guid UserId { get; set; }
    }
}
