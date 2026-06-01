public class CancelRegistrationRequest
{
    public Guid EventId { get; set; }

    public string Email { get; set; } = string.Empty;
}