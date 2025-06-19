using MediatR;
using Test.Domain.Interfaces;

namespace Test.Application.CommandsAndQueries.Orders.Commands
{
    public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IOrderRepository _repository;

        public CancelOrderHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId);
            if (order == null)
                return false;

            order.Cancel(); // Domain logic
            await _repository.UpdateAsync(order);
            return true;
        }
    }
}
