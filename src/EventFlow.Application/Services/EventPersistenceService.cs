using EventFlow.Application.Interfaces;
using EventFlow.Domain.Interfaces;
using EventFlow.Infrastructure.Persistence;

namespace EventFlow.Application.Services;

public class EventPersistenceService
{
    private readonly IEventRepository _eventRepository;
    private readonly IDataStore<EventData> _dataStore;

    public EventPersistenceService(
        IEventRepository eventRepository,
        IDataStore<EventData> dataStore)
    {
        _eventRepository = eventRepository;
        _dataStore = dataStore;
    }

    public async Task SaveAsync(
        CancellationToken cancellationToken = default)
    {
        var events = _eventRepository
            .GetAll()
            .Select(EventMapper.ToData)
            .ToList();

        await _dataStore.SaveAsync(
            events,
            cancellationToken);
    }

    public async Task LoadAsync(
        CancellationToken cancellationToken = default)
    {
        var events =
            await _dataStore.LoadAsync(
                cancellationToken);

        foreach (var eventData in events)
        {
            var eventEntity =
                EventMapper.ToDomain(eventData);

            bool alreadyExists =
                _eventRepository
                    .GetAll()
                    .Any(e => e.Id == eventEntity.Id);

            if (!alreadyExists)
            {
                _eventRepository.Add(eventEntity);
            }
        }
    }
}