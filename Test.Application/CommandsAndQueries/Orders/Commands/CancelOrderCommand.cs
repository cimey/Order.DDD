using MediatR;

namespace Test.Application.CommandsAndQueries.Orders.Commands
{
    public class CancelOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }

        public CancelOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
