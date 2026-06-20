namespace EventFlow.Domain.Entities;

public class Registration
{
    public Guid Id { get; private set; }
    public Participant Participant { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    public Registration(Participant participant)
    {
        Participant = participant
            ?? throw new ArgumentNullException(nameof(participant));

        Id = Guid.NewGuid();
        RegisteredAt = DateTime.UtcNow;
    }

    public Registration(
        Guid id,
        Participant participant,
        DateTime registeredAt)
    {
        Id = id;
        Participant = participant;
        RegisteredAt = registeredAt;
    }
}