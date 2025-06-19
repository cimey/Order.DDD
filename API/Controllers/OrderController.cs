// Controllers/OrdersController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Test.Application.CommandsAndQueries.Orders.Commands;
using Test.Application.Interfaces;
using Test.Application.Models.Order.Requests;
using Test.Domain.ValueObjects;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMediator _mediator;
    public OrdersController(IOrderService orderService, IMediator mediator)
    {
        _orderService = orderService;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request)
    {
        var address = new Address(request.Street, request.City, request.Country);
        var items = request.Items
            .Select(i => (new Product(i.ProductId, i.Name, i.Price), i.Quantity))
            .ToList();

        var orderId = await _orderService.PlaceOrder(address, request.Email, request.UserId, items);
        return Ok(new { OrderId = orderId });
    }

    [HttpPost("{orderId}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid orderId)
    {
        var result = await _mediator.Send(new CancelOrderCommand(orderId));
        return result ? Ok() : NotFound();
    }
}
