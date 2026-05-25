using EventFlow.Application.DTOs;
using EventFlow.Application.Interfaces;
using EventFlow.Domain.Common;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Interfaces;

namespace EventFlow.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public Result<Event> CreateEvent(CreateEventRequest request)
    {
        try
        {
            var venue = new Venue(
                request.VenueName,
                request.VenueAddress);

            var eventEntity = new Event(
                request.Title,
                request.Date,
                request.Capacity,
                venue,
                request.Category);

            _eventRepository.Add(eventEntity);

            return Result<Event>.Success(eventEntity);
        }
        catch (Exception ex)
        {
            return Result<Event>.Failure(ex.Message);
        }
    }

    public Result RegisterParticipant(RegisterParticipantRequest request)
    {
        var eventEntity = _eventRepository.GetById(request.EventId);

        if (eventEntity is null)
            return Result.Failure("Event not found.");

        try
        {
            var participant = new Participant(
                request.FullName,
                request.Email);

            return eventEntity.RegisterParticipant(participant);
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }

    public IEnumerable<Event> GetAllEvents()
    {
        return _eventRepository.GetAll();
    }
}