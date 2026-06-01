using EventFlow.Domain.Entities;
using EventFlow.Domain.Enums;

namespace EventFlow.Tests.Domain;

public class EventTests
{
    [Fact]
    public void CreateEvent_WithValidData_ShouldCreateEvent()
    {
        // Arrange
        var venue = new Venue("City Hall", "Main Street");

        // Act
        var eventEntity = new Event(
            "Tech Conference",
            DateTime.UtcNow.AddDays(5),
            100,
            venue,
            EventCategory.Conference);

        // Assert
        Assert.Equal("Tech Conference", eventEntity.Title);
        Assert.Equal(100, eventEntity.Capacity);
    }

    [Fact]
    public void CreateEvent_WithPastDate_ShouldThrowException()
    {
        // Arrange
        var venue = new Venue("City Hall", "Main Street");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            new Event(
                "Invalid Event",
                DateTime.UtcNow.AddDays(-1),
                10,
                venue,
                EventCategory.Conference));
    }

    [Fact]
    public void RegisterParticipant_WhenCapacityExceeded_ShouldFail()
    {
        // Arrange
        var venue = new Venue("City Hall", "Main Street");

        var eventEntity = new Event(
            "Mini Event",
            DateTime.UtcNow.AddDays(5),
            1,
            venue,
            EventCategory.Meetup);

        var participant1 = new Participant(
            "John",
            "john@test.com");

        var participant2 = new Participant(
            "Mike",
            "mike@test.com");

        // Act
        eventEntity.RegisterParticipant(participant1);

        var result = eventEntity.RegisterParticipant(participant2);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Event is full.", result.Error);
    }

    [Fact]
    public void RegisterParticipant_WithDuplicateEmail_ShouldFail()
    {
        // Arrange
        var venue = new Venue("City Hall", "Main Street");

        var eventEntity = new Event(
            "Tech Event",
            DateTime.UtcNow.AddDays(5),
            10,
            venue,
            EventCategory.Conference);

        var participant1 = new Participant(
            "John",
            "john@test.com");

        var participant2 = new Participant(
            "John Second",
            "john@test.com");

        // Act
        eventEntity.RegisterParticipant(participant1);

        var result = eventEntity.RegisterParticipant(participant2);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Participant already registered.", result.Error);
    }

    [Fact]
    public void RegisterParticipant_WithValidData_ShouldSucceed()
    {
        // Arrange
        var venue = new Venue("City Hall", "Main Street");

        var eventEntity = new Event(
            "Conference",
            DateTime.UtcNow.AddDays(5),
            10,
            venue,
            EventCategory.Conference);

        var participant = new Participant(
            "John",
            "john@test.com");

        // Act
        var result = eventEntity.RegisterParticipant(participant);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Single(eventEntity.Registrations);
    }

    [Fact]
    public void CancelRegistration_WhenParticipantExists_ShouldSucceed()
    {
        var venue = new Venue("Hall", "Address");

        var eventEntity = new Event(
            "Conference",
            DateTime.UtcNow.AddDays(5),
            10,
            venue,
            EventCategory.Conference);

        var participant =
            new Participant(
                "John",
                "john@test.com");

        eventEntity.RegisterParticipant(participant);

        var result =
            eventEntity.CancelRegistration(
                "john@test.com");

        Assert.True(result.IsSuccess);
        Assert.Empty(eventEntity.Registrations);
    }

    [Fact]
    public void CancelRegistration_WhenParticipantNotFound_ShouldFail()
    {
        var venue = new Venue("Hall", "Address");

        var eventEntity = new Event(
            "Conference",
            DateTime.UtcNow.AddDays(5),
            10,
            venue,
            EventCategory.Conference);

        var result =
            eventEntity.CancelRegistration(
                "unknown@test.com");

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Event_ShouldBecomeFull()
    {
        var venue = new Venue("Hall", "Address");

        var eventEntity = new Event(
            "Conference",
            DateTime.UtcNow.AddDays(5),
            1,
            venue,
            EventCategory.Conference);

        eventEntity.RegisterParticipant(
            new Participant(
                "John",
                "john@test.com"));

        Assert.True(eventEntity.IsFull);
    }
}