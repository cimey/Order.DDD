using MediatR;

namespace Product.Application.CommandsAndHandlers
{
    public class CreateCategoryCommand : IRequest<int>
    {

        public string Name { get; private set; }

        public int? ParentCategoryId { get; private set; }

        public CreateCategoryCommand(string name, int? parentCategoryID)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ParentCategoryId = parentCategoryID;
        }
    }
}
