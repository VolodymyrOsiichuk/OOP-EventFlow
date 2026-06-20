using EventFlow.Infrastructure.Repositories;
using EventFlow.Infrastructure.Persistence;
using EventFlow.Application.Services;
using EventFlow.Application.DTOs;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Enums;

namespace EventFlow.Tests.Integrations;

public class EventWorkflowTests
{
    [Fact]
    public void CreateAndRegister_ShouldWork()
    {
        var repository =
            new InMemoryEventRepository();

        var service =
            new EventService(repository);

        var createResult =
            service.CreateEvent(
                new CreateEventRequest
                {
                    Title = "Conference",
                    Date = DateTime.UtcNow.AddDays(5),
                    Capacity = 10,
                    VenueName = "Hall",
                    VenueAddress = "Address",
                    Category = EventCategory.Conference
                });

        var eventId =
            createResult.Value.Id;

        var registerResult =
            service.RegisterParticipant(
                new RegisterParticipantRequest
                {
                    EventId = eventId,
                    FullName = "John",
                    Email = "john@test.com"
                });

        Assert.True(registerResult.IsSuccess);
    }

    [Fact]
    public void RegisterAndCancel_ShouldRestoreCapacity()
    {
        var eventEntity =
            new Event(
                "Conference",
                DateTime.UtcNow.AddDays(5),
                1,
                new Venue("Hall", "Address"),
                EventCategory.Conference);

        eventEntity.RegisterParticipant(
            new Participant(
                "John",
                "john@test.com"));

        eventEntity.CancelRegistration(
            "john@test.com");

        Assert.Equal(
            1,
            eventEntity.AvailableSpots);
    }

    [Fact]
    public void DuplicateRegistration_ShouldFail()
    {
        var eventEntity =
            new Event(
                "Conference",
                DateTime.UtcNow.AddDays(5),
                10,
                new Venue("Hall", "Address"),
                EventCategory.Conference);

        var participant =
            new Participant(
                "John",
                "john@test.com");

        eventEntity.RegisterParticipant(
            participant);

        var result =
            eventEntity.RegisterParticipant(
                participant);

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void FullEvent_ShouldRejectRegistration()
    {
        var eventEntity =
            new Event(
                "Conference",
                DateTime.UtcNow.AddDays(5),
                1,
                new Venue("Hall", "Address"),
                EventCategory.Conference);

        eventEntity.RegisterParticipant(
            new Participant(
                "John",
                "john@test.com"));

        var result =
            eventEntity.RegisterParticipant(
                new Participant(
                    "Mike",
                    "mike@test.com"));

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task InvalidJson_ShouldReturnEmptyCollection()
    {
        var tempFile =
            Path.GetTempFileName();

        await File.WriteAllTextAsync(
            tempFile,
            "{invalid json}");

        var store =
            new JsonEventStore(tempFile);

        var result =
            await store.LoadAsync();

        Assert.Empty(result);
    }
}
