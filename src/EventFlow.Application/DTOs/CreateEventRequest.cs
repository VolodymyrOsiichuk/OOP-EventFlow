using EventFlow.Domain.Enums;

namespace EventFlow.Application.DTOs;

public class CreateEventRequest
{
    public string Title { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public int Capacity { get; set; }

    public string VenueName { get; set; } = string.Empty;

    public string VenueAddress { get; set; } = string.Empty;

    public EventCategory Category { get; set; }
}