namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } 

    public List<Post> Posts { get; set; } = [];

    public List<Comment> Comments { get; set; } = [];

    public User(int id, string username, string password)
    {
        this.Id = id;
        this.Username = username;
        this.Password = password;
    }
    
    private User(){}
}