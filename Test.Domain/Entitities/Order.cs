using Test.Domain.DomainEvents;
using Test.Domain.ValueObjects;

namespace Test.Domain.Entitities
{
    // Entities/Order.cs
    public class Order
    {
        public Guid Id { get; private set; }

        public Address ShippingAddress { get; private set; }

        public Guid? UserId { get; private set; }

        public string Email { get; private set; }

        public bool IsCancelled { get; private set; }

        private readonly List<OrderItem> _items = new();
        private readonly List<object> _domainEvents = new();


        public IReadOnlyCollection<OrderItem> Items => _items;
        public IReadOnlyCollection<object> DomainEvents => _domainEvents;

        private Order()
        {

        }
        public Order(Guid id, Address address, string email, Guid? userId)
        {
            Id = id;
            ShippingAddress = address;
            Email = email;
            UserId = userId;
        }

        public void AddItem(Product product, int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");
            _items.Add(new OrderItem(product, quantity));
        }

        public void Place()
        {
            _domainEvents.Add(new OrderPlacedEvent(Id, DateTime.UtcNow, UserId ?? Guid.Empty, Email,
                Items.Select(x => new BuildingBlocks.Model.Dtos.Order.OrderItemDto
                {
                    ProductId = x.Product.Id,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Quantity = x.Quantity
                }), new BuildingBlocks.Model.Dtos.Order.AddressDto(ShippingAddress.City, ShippingAddress.Country, ShippingAddress.Street)));
        }

        public void Cancel()
        {
            if (IsCancelled) throw new InvalidOperationException("Order is already cancelled.");
            IsCancelled = true;

            _domainEvents.Add(new OrderCancelledEvent(Id, DateTime.UtcNow));
        }

        public void ClearDomainEvents() => _domainEvents.Clear();
        public decimal GetTotalAmount() => _items.Sum(i => i.GetTotal());
    }
}
