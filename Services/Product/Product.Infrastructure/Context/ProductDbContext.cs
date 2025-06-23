using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using ProductEntity = Product.Domain.Aggregates.Product;

namespace Product.Infrastructure.Context
{
    public class ProductDbContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().ToTable("Product").OwnsOne(p => p.Price);
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
