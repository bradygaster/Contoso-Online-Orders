using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<CartItem>();

            // one-to-many: Order <-> CartItem
            modelBuilder.Entity<Order>()
                        .HasMany<CartItem>(x => x.Items);

            // seed the database with sample data
            modelBuilder.Entity<Product>().HasData(new Product[] {
                new Product
                {
                    Id = 1,
                    InventoryCount = 20,
                    Name = "Compact Disks"
                },
                new Product
                {
                    Id = 2,
                    InventoryCount = 10,
                    Name = "DVDs"
                },
                new Product
                {
                    Id = 3,
                    InventoryCount = 200,
                    Name = "Floppy Disks"
                }
            });
        }

        public DbSet<Server.Data.Product> Products { get; set; }
        public DbSet<Server.Data.Order> Order { get; set; }
    }
}