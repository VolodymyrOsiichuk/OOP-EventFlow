using EventFlow.Domain.Enums;

namespace EventFlow.Infrastructure.Persistence;


public class EventData
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public int Capacity { get; set; }

    public string VenueName { get; set; } = string.Empty;

    public string VenueAddress { get; set; } = string.Empty;

    public EventCategory Category { get; set; }

    public List<RegistrationData> Registrations { get; set; }
        = [];
}