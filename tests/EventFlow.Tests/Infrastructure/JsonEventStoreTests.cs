using EventFlow.Infrastructure.Persistence;

namespace EventFlow.Tests.Infrastructure;

public class JsonEventStoreTests
{
    [Fact]
    public async Task SaveAsync_ShouldCreateFile()
    {
        var store =
            new JsonEventStore(
                "test-events.json");

        var events =
            new List<EventData>();

        await store.SaveAsync(events);

        Assert.True(
            File.Exists(
                "test-events.json"));
    }

    [Fact]
    public async Task LoadAsync_WhenFileMissing_ShouldReturnEmptyCollection()
    {
        var store =
            new JsonEventStore(
                "missing-file.json");

        var result =
            await store.LoadAsync();

        Assert.Empty(result);
    }
}
