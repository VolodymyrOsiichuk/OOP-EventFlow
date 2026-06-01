using EventFlow.Infrastructure.Repositories;
using EventFlow.Application.Services;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Enums;


namespace EventFlow.Tests.Application;


public class EventQueryServiceTests
{
    [Fact]
    public void GetActiveEvents_ShouldReturnFutureEvents()
    {
        var repository =
            new InMemoryEventRepository();

        var service =
            new EventQueryService(repository);

        repository.Add(
            new Event(
                "Conference",
                DateTime.UtcNow.AddDays(10),
                10,
                new Venue("Hall", "Address"),
                EventCategory.Conference));

        var result =
            service.GetActiveEvents();

        Assert.Single(result);
    }

    [Fact]
    public void GetEventsByCategory_ShouldReturnCorrectEvents()
    {
        var repository =
            new InMemoryEventRepository();

        repository.Add(
            new Event(
                "Conference",
                DateTime.UtcNow.AddDays(5),
                10,
                new Venue("Hall", "Address"),
                EventCategory.Conference));

        var service =
            new EventQueryService(repository);

        var result =
            service.GetEventsByCategory(
                EventCategory.Conference);

        Assert.Single(result);
    }

    
}