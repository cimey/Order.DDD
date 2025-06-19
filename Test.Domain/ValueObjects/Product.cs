namespace Test.Domain.ValueObjects
{
    public record Product(Guid Id, string Name, decimal Price);
    public record Address(string Street, string City, string Country);
}
