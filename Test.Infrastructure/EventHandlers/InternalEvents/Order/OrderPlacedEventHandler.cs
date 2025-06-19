using BuildingBlocks.Model.Order;
using MassTransit;
using MediatR;
using Test.Domain.DomainEvents;

namespace Test.Infrastructure.EventHandlers.InternalEvents.Order
{
    public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderPlacedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
        {

            Console.WriteLine($"[EVENT via MediatR] Order {notification.OrderId} placed at {notification.PlacedAt}");

            await _publishEndpoint.Publish(new OrderPlacedExternalEvent
            {
                Address = notification.Address,
                Email = notification.Email,
                OrderDate = notification.PlacedAt.Date,
                OrderId = notification.OrderId,
                OrderItems = notification.OrderItems,
                Price = notification.Price,
                ProductName = notification.ProductName,
                UserId = notification.UserId

            });
        }
    }
}
