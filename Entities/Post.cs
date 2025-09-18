namespace Entities;

public class Post(int? id, string title, string body, int userId)
{
    public int? id { get; set; } = id;
    public string title { get; set; } = title;
    public string body { get; set; } = body;
    public int userId { get; set; } = userId;
}

