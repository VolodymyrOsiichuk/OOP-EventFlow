namespace EventFlow.Domain.Entities;

public class Participant
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }

    public Participant(string fullName, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Name cannot be empty.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        Id = Guid.NewGuid();
        FullName = fullName;
        Email = email;
    }
}