using MediatR;

namespace Test.Domain.DomainEvents
{
    public class OrderCancelledEvent : INotification
    {
        public Guid OrderId { get; private set; }

        public DateTimeOffset CancellationDate { get; private set; }

        public OrderCancelledEvent(Guid OrderId, DateTimeOffset cancellationTime)
        {
            this.OrderId = OrderId;
            CancellationDate = cancellationTime;
        }
    }
}
