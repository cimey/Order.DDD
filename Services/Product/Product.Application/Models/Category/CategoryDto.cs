namespace Product.Application.Models.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParentCategoryName { get; set; }
        public int? ParentCategoryId{ get; set; }

        public DateTimeOffset CreatedAt{ get; set; }
        public string? CreatedBy { get; set; }
    }
}
