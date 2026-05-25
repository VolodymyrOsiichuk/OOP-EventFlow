using EventFlow.Domain.Common;
using EventFlow.Domain.Enums;

namespace EventFlow.Domain.Entities;

public class Event
{
    private readonly List<Registration> _registrations = [];

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public DateTime Date { get; private set; }

    public int Capacity { get; private set; }

    public Venue Venue { get; private set; }

    public EventCategory Category { get; private set; }

    public IReadOnlyCollection<Registration> Registrations
        => _registrations.AsReadOnly();

    public int AvailableSpots
        => Capacity - _registrations.Count;

    public Event(
        string title,
        DateTime date,
        int capacity,
        Venue venue,
        EventCategory category)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");

        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.");

        if (date < DateTime.UtcNow)
            throw new ArgumentException("Event date cannot be in the past.");

        Id = Guid.NewGuid();
        Title = title;
        Date = date;
        Capacity = capacity;
        Venue = venue;
        Category = category;
    }

    public Result RegisterParticipant(Participant participant)
    {
        if (participant is null)
            return Result.Failure("Participant is null.");

        if (_registrations.Count >= Capacity)
            return Result.Failure("Event is full.");

        bool alreadyRegistered = _registrations
            .Any(r => r.Participant.Email == participant.Email);

        if (alreadyRegistered)
            return Result.Failure("Participant already registered.");

        _registrations.Add(new Registration(participant));

        return Result.Success();
    }
}