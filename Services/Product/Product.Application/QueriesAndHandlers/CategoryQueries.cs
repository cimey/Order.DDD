using MediatR;
using Product.Application.Models.Category;

namespace Product.Application.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; private set; }
        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
    }
}
