using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Domain.Entitities;
using Test.Domain.Interfaces;
using Test.Infrastructure.Data;

namespace Test.Infrastructure.Repositories;

public class EFOrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;
    private readonly IMediator _mediator;

    public EFOrderRepository(OrderDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public Order? GetById(Guid id)
    {
        return _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefault(o => o.Id == id);
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
           .Include(o => o.Items)
           .ThenInclude(i => i.Product)
           .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task Save(Order order)
    {
        _context.Orders.Add(order);

        await SaveChangesAsync(order);

    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await SaveChangesAsync(order);
    }

    private async Task SaveChangesAsync(Order order)
    {
        await _context.SaveChangesAsync();
        foreach (var domainEvent in order.DomainEvents)
        {
            await _mediator.Publish(domainEvent); // async event publishing
        }
    }
}
