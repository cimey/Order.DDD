using BuildingBlocks.Model.Order;
using MassTransit;

namespace Notification.Consumer.Consumers.Order
{
    public class OrderPlacedEventConsumer : IConsumer<OrderPlacedExternalEvent>
    {
        private readonly IEmailSender _emailSender;

        public OrderPlacedEventConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }
        public async Task Consume(ConsumeContext<OrderPlacedExternalEvent> context)
        {
            var msg = context.Message;
            await _emailSender.SendAsync(
                to: msg.Email,
                subject: "Order Confirmation",
                body: $"Hi, your order for {msg.ProductName} (${msg.Price}) was placed on {msg.OrderDate}."
            );
        }
    }
}
