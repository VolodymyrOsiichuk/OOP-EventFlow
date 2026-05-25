using EventFlow.Application.DTOs;
using EventFlow.Application.Services;
using EventFlow.Domain.Enums;
using EventFlow.Infrastructure.Repositories;

var repository = new InMemoryEventRepository();

var eventService = new EventService(repository);

Console.WriteLine("=== EVENT FLOW ===");

var createResult = eventService.CreateEvent(
    new CreateEventRequest
    {
        Title = "Tech Conference 2026",
        Date = DateTime.UtcNow.AddDays(10),
        Capacity = 2,
        VenueName = "City Hall",
        VenueAddress = "Main Street 10",
        Category = EventCategory.Conference
    });

if (!createResult.IsSuccess)
{
    Console.WriteLine(createResult.Error);
    return;
}

var createdEvent = createResult.Value;

Console.WriteLine("Event created successfully.");
Console.WriteLine($"Event ID: {createdEvent.Id}");

var registrationResult = eventService.RegisterParticipant(
    new RegisterParticipantRequest
    {
        EventId = createdEvent.Id,
        FullName = "John Smith",
        Email = "john@example.com"
    });

Console.WriteLine();

if (registrationResult.IsSuccess)
{
    Console.WriteLine("Participant registered successfully.");
}
else
{
    Console.WriteLine(registrationResult.Error);
}

Console.WriteLine();
Console.WriteLine("=== EVENTS ===");

foreach (var eventItem in eventService.GetAllEvents())
{
    Console.WriteLine($"Title: {eventItem.Title}");
    Console.WriteLine($"Category: {eventItem.Category}");
    Console.WriteLine($"Available spots: {eventItem.AvailableSpots}");
    Console.WriteLine($"Registrations: {eventItem.Registrations.Count}");
    Console.WriteLine();
}