using EventFlow.Application.DTOs;
using EventFlow.Domain.Common;
using EventFlow.Domain.Entities;

namespace EventFlow.Application.Interfaces;

public interface IEventService
{
    Result<Event> CreateEvent(CreateEventRequest request);

    Result RegisterParticipant(RegisterParticipantRequest request);

    IEnumerable<Event> GetAllEvents();

    Result CancelRegistration(Guid eventId, string email);
}