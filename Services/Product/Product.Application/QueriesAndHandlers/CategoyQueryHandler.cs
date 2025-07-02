using MediatR;
using Product.Application.Models.Category;
using Product.Application.Queries;
using Product.Domain.Entities;

namespace Product.Application.QueriesAndHandlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }
        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryName = category.ParentCategory?.Name ?? string.Empty,
                ParentCategoryId = category.ParentCategoryId,
                CreatedAt = category.CreatedAt,
                CreatedBy = category.CreatedBy
            };
        }
    }

    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }
        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                ParentCategoryName = c.ParentCategory?.Name ?? string.Empty,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy
            });
        }
    }
}
