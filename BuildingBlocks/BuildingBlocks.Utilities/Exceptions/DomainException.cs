namespace BuildingBlocks.Utilities.Exceptions
{
    public class DomainException : Exception
    {

        public DomainException(string message) : base(message)
        {
        }
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public DomainException() : base("A domain exception has occurred.")
        {
        }
    }
}
