using Test.Domain.ValueObjects;

namespace Test.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Guid> PlaceOrder(Address address, string email, Guid? userId, List<(Product product, int quantity)> items);
    }
}
