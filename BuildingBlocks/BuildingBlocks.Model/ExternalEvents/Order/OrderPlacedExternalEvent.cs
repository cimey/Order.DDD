using BuildingBlocks.Model.Dtos.Order;

namespace BuildingBlocks.Model.Order
{
    public class OrderPlacedExternalEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }

        public AddressDto Address { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
