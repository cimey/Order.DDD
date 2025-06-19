using Test.Application.Models.Order.Dtos;

namespace Test.Application.Models.Order.Requests
{
    public class PlaceOrderRequest
    {
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string Country { get; set; } = "";

        public Guid UserId { get; set; }

        public string Email { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
