using Test.Application.Interfaces;
using Test.Domain.Entitities;
using Test.Domain.Interfaces;
using Test.Domain.ValueObjects;

namespace Test.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> PlaceOrder(Address address, string email, Guid? userId, List<(Product product, int quantity)> items)
        {
            var order = new Order(Guid.NewGuid(), address, email, userId);
            foreach (var (product, quantity) in items)
            {
                order.AddItem(product, quantity);
            }

            order.Place();

            await _repository.Save(order);
            return order.Id;
        }
    }
}
