using EventFlow.Infrastructure.Repositories;
using EventFlow.Infrastructure.Persistence;
using EventFlow.Application.Services;
using EventFlow.Domain.Entities;
using EventFlow.Domain.Enums;

namespace EventFlow.Tests.Integrations;

public class PersistenceTests
{
    [Fact]
    public async Task SaveAndLoad_ShouldPreserveEvent()
    {
        var tempFile = Path.GetTempFileName();

        var repository =
            new InMemoryEventRepository();

        var store =
            new JsonEventStore(tempFile);

        var persistence =
            new EventPersistenceService(
                repository,
                store);

        repository.Add(
            new Event(
                "Conference",
                DateTime.UtcNow.AddDays(5),
                100,
                new Venue("Hall", "Address"),
                EventCategory.Conference));

        await persistence.SaveAsync();

        var newRepository =
            new InMemoryEventRepository();

        var newPersistence =
            new EventPersistenceService(
                newRepository,
                new JsonEventStore(tempFile));

        await newPersistence.LoadAsync();

        Assert.Single(
            newRepository.GetAll());
    }

    [Fact]
    public async Task LoadAsync_WhenFileMissing_ShouldNotThrow()
    {
        var path =
            Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid() + ".json");

        var repository =
            new InMemoryEventRepository();

        var persistence =
            new EventPersistenceService(
                repository,
                new JsonEventStore(path));

        await persistence.LoadAsync();

        Assert.Empty(
            repository.GetAll());
    }
}