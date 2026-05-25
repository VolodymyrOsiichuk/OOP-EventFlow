namespace EventFlow.Domain.Entities;

public class Venue
{
    public string Name { get; private set; }
    public string Address { get; private set; }

    public Venue(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Venue name cannot be empty.");

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be empty.");

        Name = name;
        Address = address;
    }
}