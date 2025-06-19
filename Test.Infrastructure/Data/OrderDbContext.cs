using Microsoft.EntityFrameworkCore;
using Test.Domain.Entitities;

namespace Test.Infrastructure.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(order =>
            {
                order.HasKey(o => o.Id);

                order.OwnsOne(o => o.ShippingAddress);

                order.OwnsMany(o => o.Items, item =>
                {
                    item.WithOwner().HasForeignKey("OrderId");
                    item.Property(i => i.Quantity);
                    item.OwnsOne(i => i.Product);
                });
            });
        }
    }
}
