using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;

namespace EventFlow.Infrastructure.Repositories;

public class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _events = [];

    public void Add(Event entity)
    {
        _events.Add(entity);
    }

    public Event? GetById(Guid id)
    {
        return _events.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Event> GetAll()
    {
        return _events;
    }
}