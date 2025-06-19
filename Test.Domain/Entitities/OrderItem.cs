using Test.Domain.ValueObjects;

namespace Test.Domain.Entitities
{
    public class OrderItem
    {
        public Product Product { get; }
        public int Quantity { get; }

        private OrderItem()
        {
            
        }
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal GetTotal() => Product.Price * Quantity;
    }

}
