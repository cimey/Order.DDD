using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BuildingBlocks.Model.Data;
using BuildingBlocks.Utilities.Exceptions;

namespace Product.Domain.Entities
{
    public class Category : EntityBase<int>
    {

        private Category()
        {

        }

        public Category(int id, string name, int? parentCategoryId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Category name cannot be empty.");
            if (name.Length > 100)
                throw new DomainException("Category name cannot exceed 100 characters.");
            Name = name;
            ParentCategoryId = parentCategoryId;

            CreatedAt = DateTimeOffset.UtcNow;
            CreatedBy = "system"; // This should be set based on your application's user context
        }


        [MaxLength(100)]
        public string Name { get; private set; }

        public int? ParentCategoryId { get; private set; }

        [ForeignKey(nameof(ParentCategoryId))]
        public Category? ParentCategory { get; private set; }


        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Category name cannot be empty.");
            if (name.Length > 100)
                throw new DomainException("Category name cannot exceed 100 characters.");
            Name = name;
            UpdatedAt = DateTimeOffset.UtcNow;
            UpdatedBy = "system";
        }

        public void UpdateParentCategory(int? parentCategoryId)
        {
            ParentCategoryId = parentCategoryId;

            UpdatedAt = DateTimeOffset.UtcNow;
            UpdatedBy = "system"; // This should be set based on your application's user context
        }
    }
}
