namespace DTOs.Users;

public record RegisterRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}