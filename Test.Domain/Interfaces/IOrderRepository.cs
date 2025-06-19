using Test.Domain.Entitities;

namespace Test.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Order? GetById(Guid id);
        Task<Order?> GetByIdAsync(Guid id);
        Task UpdateAsync(Order order);
        Task Save(Order order);
    }
}
