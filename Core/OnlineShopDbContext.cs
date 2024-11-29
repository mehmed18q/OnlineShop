using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class OnlineShopDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Product>()
                 .Property(p => p.ProductName)
                 .HasColumnName("Title")
                 .IsRequired();

            _ = modelBuilder.Entity<Product>().ToTable("Products", "Base");
        }
    }
}
