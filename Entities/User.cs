namespace Entities;

public class User(int? id, string username, string password)
{
    public int? Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}