using Product.Domain.Entities;

namespace Product.Test.Domain
{
    public class CategoryEntityTests
    {
        [Fact]
        public void CategoryUpdateThrowsException_WhenParentCategoryId_SameWithId()
        {
            // Arrange  
            var exception = Assert.Throws<InvalidOperationException>(() => new Category(1, "Test Category", 1));

            // Assert  
            Assert.Equal("A category cannot be its own parent.", exception.Message);
        }
    }
}
