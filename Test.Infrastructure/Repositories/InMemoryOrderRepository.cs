using Test.Domain.Entitities;
using Test.Domain.Interfaces;

namespace Test.Infrastructure.Repositories
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly Dictionary<Guid, Order> _orders = new();

        public Order? GetById(Guid id) => _orders.TryGetValue(id, out var order) ? order : null;

        public Task<Order?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Save(Order order)
        {
            _orders[order.Id] = order;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
