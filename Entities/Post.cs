using DTOs.Comments;
using DTOs.Posts;

namespace Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    //for EFC
    private Post(){}

    public Post( string title, string body, int userId)
    {
        this.Title = title;
        this.Body = body;
        this.UserId = userId;
    }

    public static PostDto ToDto(Post post, string username, List<CommentDto> comments)
    {
        return new PostDto(post.Id, post.Title, post.Body, post.UserId,
            username, comments);
    }
}

