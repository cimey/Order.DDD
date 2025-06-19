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
        }


        [MaxLength(100)]
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }

        [ForeignKey(nameof(ParentCategoryId))]
        public Category? ParentCategory { get; set; }
    }
}
