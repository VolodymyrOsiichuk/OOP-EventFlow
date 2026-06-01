using System.Text.Json;
using EventFlow.Application.Interfaces;

namespace EventFlow.Infrastructure.Persistence;

public class JsonEventStore
    : IDataStore<EventData>
{
    private readonly string _filePath;

    public JsonEventStore(string filePath)
    {
        _filePath = filePath;
    }

    public async Task SaveAsync(
        IReadOnlyCollection<EventData> items,
        CancellationToken cancellationToken = default)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        await using var stream =
            File.Create(_filePath);

        await JsonSerializer.SerializeAsync(
            stream,
            items,
            options,
            cancellationToken);
    }

    public async Task<IReadOnlyCollection<EventData>> LoadAsync(
        CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        try
        {
            await using var stream =
                File.OpenRead(_filePath);

            var result =
                await JsonSerializer.DeserializeAsync<
                    List<EventData>>(
                    stream,
                    cancellationToken: cancellationToken);

            return result ?? [];
        }
        catch (JsonException)
        {
            return [];
        }
    }
}