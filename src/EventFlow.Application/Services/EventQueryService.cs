using EventFlow.Domain.Entities;
using EventFlow.Domain.Enums;
using EventFlow.Domain.Interfaces;

namespace EventFlow.Application.Services;

public class EventQueryService
{
    private readonly IEventRepository _eventRepository;

    public EventQueryService(
        IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public IEnumerable<Event> GetActiveEvents()
    {
        return _eventRepository
            .GetAll()
            .Where(e => e.Date > DateTime.UtcNow);
    }

    public IEnumerable<Event> GetEventsByCategory(
        EventCategory category)
    {
        return _eventRepository
            .GetAll()
            .Where(e => e.Category == category);
    }

    public IEnumerable<Event> GetMostPopularEvents()
    {
        return _eventRepository
            .GetAll()
            .OrderByDescending(
                e => e.Registrations.Count);
    }

    public Dictionary<EventCategory, int>
        GetEventsCountByCategory()
    {
        return _eventRepository
            .GetAll()
            .GroupBy(e => e.Category)
            .ToDictionary(
                g => g.Key,
                g => g.Count());
    }
}