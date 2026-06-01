namespace EventFlow.Infrastructure.Persistence;


public class RegistrationData
{
    public Guid Id { get; set; }

    public DateTime RegisteredAt { get; set; }

    public ParticipantData Participant { get; set; }
        = new();
}