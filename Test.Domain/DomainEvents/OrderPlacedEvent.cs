using BuildingBlocks.Model.Dtos.Order;
using MediatR;

namespace Test.Domain.DomainEvents
{
    public class OrderPlacedEvent : INotification
    {
        public Guid OrderId { get; }

        public DateTime PlacedAt { get; }

        public Guid UserId { get; set; }

        public string Email { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; }

        public AddressDto Address { get; set; }

        public OrderPlacedEvent(Guid orderId, DateTime placedAt, Guid userId, string email, IEnumerable<OrderItemDto> orderItems, AddressDto address)
        {
            OrderId = orderId;
            PlacedAt = placedAt;
            UserId = userId;
            OrderItems = orderItems;
            Email = email;
            Address = address;
        }
    }
}
