using EventFlow.Domain.Entities;

namespace EventFlow.Infrastructure.Persistence;

public static class EventMapper
{
    public static EventData ToData(Event eventEntity)
    {
        return new EventData
        {
            Id = eventEntity.Id,
            Title = eventEntity.Title,
            Date = eventEntity.Date,
            Capacity = eventEntity.Capacity,
            VenueName = eventEntity.Venue.Name,
            VenueAddress = eventEntity.Venue.Address,
            Category = eventEntity.Category,

            Registrations = eventEntity.Registrations
                .Select(r => new RegistrationData
                {
                    Id = r.Id,
                    RegisteredAt = r.RegisteredAt,

                    Participant = new ParticipantData
                    {
                        Id = r.Participant.Id,
                        FullName = r.Participant.FullName,
                        Email = r.Participant.Email
                    }
                })
                .ToList()
        };
    }

    public static Event ToDomain(EventData data)
    {
        var venue = new Venue(
            data.VenueName,
            data.VenueAddress);

        var eventEntity = new Event(
            data.Id,
            data.Title,
            data.Date,
            data.Capacity,
            venue,
            data.Category);

        foreach (var registrationData in data.Registrations)
        {
            var participant = new Participant(
                registrationData.Participant.Id,
                registrationData.Participant.FullName,
                registrationData.Participant.Email);

            var registration = new Registration(
                registrationData.Id,
                participant,
                registrationData.RegisteredAt);

            eventEntity.RestoreRegistration(registration);
        }

        return eventEntity;
    }
}