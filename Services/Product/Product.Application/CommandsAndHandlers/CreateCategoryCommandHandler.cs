using MediatR;
using Product.Domain.Entities;

namespace Product.Application.CommandsAndHandlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }
        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category(0, request.Name, request.ParentCategoryId);

            return await _categoryRepository.AddCategoryAsync(category);
        }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(request.Id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {request.Id} not found.");
            }


            if ((await _categoryRepository.GetAllAncestorsAsync(request.ParentCategoryId)).Contains(request.Id))
                throw new InvalidOperationException("Cyclic parent relationship detected.");
            
            category.UpdateParentCategory(request.ParentCategoryId);
            category.UpdateName(request.Name);


            await _categoryRepository.UpdateCategoryAsync(category);
            return true;
        }
    }
}
