using EventFlow.Application.DTOs;
using EventFlow.Application.Services;
using EventFlow.Domain.Enums;
using EventFlow.Infrastructure.Repositories;

namespace EventFlow.Tests.Application;

public class EventServiceTests
{
    [Fact]
    public void CreateEvent_ShouldAddEventToRepository()
    {
        // Arrange
        var repository = new InMemoryEventRepository();

        var service = new EventService(repository);

        var request = new CreateEventRequest
        {
            Title = "Service Test Event",
            Date = DateTime.UtcNow.AddDays(3),
            Capacity = 50,
            VenueName = "Conference Hall",
            VenueAddress = "Main Street",
            Category = EventCategory.Conference
        };

        // Act
        var result = service.CreateEvent(request);

        // Assert
        Assert.True(result.IsSuccess);

        var events = service.GetAllEvents();

        Assert.Single(events);
    }
}