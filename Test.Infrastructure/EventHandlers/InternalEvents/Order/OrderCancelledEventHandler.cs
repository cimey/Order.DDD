using MediatR;
using Test.Domain.DomainEvents;

namespace Test.Infrastructure.EventHandlers.InternalEvents.Order
{
    public class OrderCancelledEventHandler : INotificationHandler<OrderCancelledEvent>
    {
        public Task Handle(OrderCancelledEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"[EVENT via MediatR] Order {notification.OrderId} cancelled at {notification.CancellationDate}");
            // Optionally: send email, publish to message bus, etc.
            return Task.CompletedTask;
        }
    }
}
