using BuildingBlocks.Utilities.Exceptions;

namespace Product.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }


        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new DomainException("Amount cannot be negative.");
            if (string.IsNullOrWhiteSpace(currency))
                throw new DomainException("Currency is required.");

            Amount = amount;
            Currency = currency.ToUpper();
        }

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot add money with different currencies.");
            return new Money(Amount + other.Amount, Currency);
        }
        public Money Subtract(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot subtract money with different currencies.");
            return new Money(Amount - other.Amount, Currency);
        }
        public override string ToString() => $"{Amount} {Currency}";
    }
}
