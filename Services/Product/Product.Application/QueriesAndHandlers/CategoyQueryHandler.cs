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
                CreatedAt = category.CreatedAt,
                CreatedBy = category.CreatedBy
            };
        }
    }
}
