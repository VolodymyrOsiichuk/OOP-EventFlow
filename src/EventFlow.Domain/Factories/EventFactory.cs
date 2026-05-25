using EventFlow.Domain.Entities;
using EventFlow.Domain.Enums;

namespace EventFlow.Domain.Factories;

public static class EventFactory
{
    public static Event CreateConference(
        string title,
        DateTime date,
        int capacity,
        Venue venue)
    {
        return new Event(
            title,
            date,
            capacity,
            venue,
            EventCategory.Conference);
    }

    public static Event CreateConcert(
        string title,
        DateTime date,
        int capacity,
        Venue venue)
    {
        return new Event(
            title,
            date,
            capacity,
            venue,
            EventCategory.Concert);
    }
}