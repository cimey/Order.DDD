using MediatR;

namespace Product.Application.CommandsAndHandlers
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public int? ParentCategoryId { get; private set; }

        public UpdateCategoryCommand(int id, string name, int? parentCategoryId)
        {
            Id = id;
            Name = name;
            ParentCategoryId = parentCategoryId == 0 ? null : parentCategoryId;
        }
    }
}
