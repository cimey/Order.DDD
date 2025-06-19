using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Model.Data;
using BuildingBlocks.Utilities.Exceptions;
using Product.Domain.Entities;
using Product.Domain.ValueObjects;

namespace Product.Domain.Aggregates
{
    public class Product : EntityBase<Guid>
    {
        [MaxLength(100)]
        public string Name { get; private set; } = string.Empty;

        public string Sku { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public bool IsActive { get; private set; }

        public bool IsAvailable { get; private set; }

        public int CategoryId { get; private set; }

        public Money Price { get; private set; } = new Money(0, "USD");

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; private set; }

        private Product()
        {
            
        }
        public Product(Guid id, string name, string sku, Money price, string description, int categoryId)
        {
            Id = id;
            SetName(name);
            SetSku(sku);
            SetPrice(price);
            Description = description;
            IsAvailable = true;
            CreatedAt = DateTime.UtcNow;
            CategoryId = categoryId;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Product name cannot be empty.");

            if (name.Length > 100)
                throw new DomainException("Product name cannot exceed 100 characters.");
            Name = name;
        }

        public void SetSku(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new DomainException("SKU is required.");
            Sku = sku;
        }

        public void SetPrice(Money price)
        {
            if (price == null || price.Amount <= 0)
                throw new DomainException("Invalid price.");
            Price = price;
        }

        public void Deactivate()
        {
            IsAvailable = false;
        }
    }
}
