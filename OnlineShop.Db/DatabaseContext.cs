using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                        new Product
                        {
                            Id = new Guid("8ccac07f-6322-4850-a6ab-954e873c485f"),
                            Name = "Яблоки Голден",
                            Cost = 89.99m,
                            Description = "У яблок сорта Голден золотистая кожура с мелкими коричневыми точками. Под ней — сочная, сладкая и душистая мякоть с медовыми нотками во вкусе и лёгкими пряными оттенками в аромате.",
                            IconSource = "https://d2j6dbq0eux0bg.cloudfront.net/images/67958791/2757175015.jpg"
                        },
                        new Product
                        {
                            Id = new Guid("486951c4-63a2-42f0-9681-de01f0e7bba3"),
                            Name = "Апельсины",
                            Cost = 149.99m,
                            Description = "Насыщенный вкус с выразительной кислинкой — главное, за что любят апельсины. Плотная, приятная на ощупь цедра скрывает нежную и очень сочную мякоть. Слегка пьянящий, душистый аромат дополняет это цитрусовое удовольствие.",
                            IconSource = "https://i.servimg.com/u/f62/19/52/39/22/orange10.jpg"
                        },
                        new Product
                        {
                            Id = new Guid("7c0912d4-01a2-435b-b695-e2b9da93c009"),
                            Name = "Бананы",
                            Cost = 109.99m,
                            Description = "Питательный фрукт с мягкой, нежной текстурой, выразительным сладким вкусом и насыщенным ароматом. Банан легко чистится от своей золотисто-жёлтой кожуры, обнажая бежевую мякоть.",
                            IconSource = "https://iberiamercaexport.es/images/banana.jpg"
                        }
                    );
        }
    }
}
