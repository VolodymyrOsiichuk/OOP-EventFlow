using EventFlow.Domain.Enums;

namespace EventFlow.Domain.Entities;

public class Ticket
{
    public Guid Id { get; private set; }
    public TicketType Type { get; private set; }
    public decimal Price { get; private set; }

    public Ticket(TicketType type, decimal price)
    {
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.");

        Id = Guid.NewGuid();
        Type = type;
        Price = price;
    }
}