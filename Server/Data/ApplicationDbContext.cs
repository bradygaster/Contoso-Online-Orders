using Microsoft.EntityFrameworkCore;

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

            // Many-to-many: Order <-> CartItem
            modelBuilder.Entity<Order>()
                        .HasMany<CartItem>(x => x.Cart);
        }

        public DbSet<Product> Products { get; set; }
    }
}