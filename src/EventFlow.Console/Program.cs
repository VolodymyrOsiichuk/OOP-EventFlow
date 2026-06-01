using EventFlow.Application.DTOs;
using EventFlow.Application.Services;
using EventFlow.Domain.Enums;
using EventFlow.Infrastructure.Persistence;
using EventFlow.Infrastructure.Repositories;

var repository = new InMemoryEventRepository();

var eventService = new EventService(repository);

var queryService = new EventQueryService(repository);

var store = new JsonEventStore("events.json");

var persistenceService =
    new EventPersistenceService(
        repository,
        store);

await persistenceService.LoadAsync();

bool running = true;

while (running)
{
    Console.Clear();

    Console.WriteLine("=== EVENT FLOW ===");
    Console.WriteLine("1. Create Event");
    Console.WriteLine("2. Register Participant");
    Console.WriteLine("3. Cancel Registration");
    Console.WriteLine("4. View All Events");
    Console.WriteLine("5. Active Events");
    Console.WriteLine("6. Statistics");
    Console.WriteLine("7. Save Data");
    Console.WriteLine("0. Exit");

    Console.Write("Choose option: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            CreateEvent(eventService);
            break;

        case "2":
            RegisterParticipant(eventService); 
            break;

        case "3":
            CancelRegistration(eventService);
            break;

        case "4":
            ShowAllEvents(eventService);
            break;

        case "5":
            ShowActiveEvents(queryService);
            break;

        case "6":
            ShowStatistics(queryService);
            break;

        case "7":
            await persistenceService.SaveAsync();

            Console.WriteLine("Data saved.");

            Pause();
            break;

        case "0":
            await persistenceService.SaveAsync();

            running = false;
            break;
    }
}

static void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press any key...");
    Console.ReadKey();
}

static void CreateEvent(
    EventService service)
{
    Console.Write("Title: ");
    var title = Console.ReadLine()!;

    var request =
        new CreateEventRequest
        {
            Title = title,
            Date = DateTime.UtcNow.AddDays(5),
            Capacity = 50,
            VenueName = "Conference Hall",
            VenueAddress = "Main Street",
            Category = EventCategory.Conference
        };

    var result =
        service.CreateEvent(request);

    Console.WriteLine(
        result.IsSuccess
            ? "Event created."
            : result.Error);

    Pause();
}

static void ShowAllEvents(
    EventService service)
{
    foreach (var e in service.GetAllEvents())
    {
        Console.WriteLine();
        Console.WriteLine($"Id: {e.Id}");
        Console.WriteLine($"Title: {e.Title}");
        Console.WriteLine($"Category: {e.Category}");
        Console.WriteLine($"Participants: {e.Registrations.Count}");
        Console.WriteLine($"Available spots: {e.AvailableSpots}");
    }

    Pause();
}

static void ShowActiveEvents(
    EventQueryService service)
{
    foreach (var e in service.GetActiveEvents())
    {
        Console.WriteLine(
            $"{e.Title} ({e.Date:d})");
    }

    Pause();
}

static void ShowStatistics(
    EventQueryService service)
{
    var stats =
        service.GetEventsCountByCategory();

    foreach (var item in stats)
    {
        Console.WriteLine(
            $"{item.Key}: {item.Value}");
    }

    Pause();
}

static void RegisterParticipant(
    EventService service)
{
    Console.Write("Event Id: ");

    if (!Guid.TryParse(
        Console.ReadLine(),
        out var eventId))
    {
        Console.WriteLine("Invalid id.");
        Pause();
        return;
    }

    Console.Write("Participant name: ");
    var name = Console.ReadLine()!;

    Console.Write("Email: ");
    var email = Console.ReadLine()!;

    var result =
        service.RegisterParticipant(
            new RegisterParticipantRequest
            {
                EventId = eventId,
                FullName = name,
                Email = email
            });

    Console.WriteLine(
        result.IsSuccess
            ? "Participant registered."
            : result.Error);

    Pause();
}

static void CancelRegistration(
    EventService service)
{
    Console.Write("Event Id: ");

    if (!Guid.TryParse(
        Console.ReadLine(),
        out var eventId))
    {
        Console.WriteLine("Invalid id.");
        Pause();
        return;
    }

    Console.Write("Email: ");

    var email = Console.ReadLine()!;

    var result =
        service.CancelRegistration(
            eventId,
            email);

    Console.WriteLine(
        result.IsSuccess
            ? "Registration cancelled."
            : result.Error);

    Pause();
}