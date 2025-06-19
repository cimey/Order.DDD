using System;

namespace BuildingBlocks.Model.Dtos.Order
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
