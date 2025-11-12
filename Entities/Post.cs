using DTOs.Comments;
using DTOs.Posts;

namespace Entities;

public class Post(int id, string title, string body, int userId)
{
    public int Id { get; set; } = id;
    public string Title { get; set; } = title;
    public string Body { get; set; } = body;
    public int UserId { get; set; } = userId;

    public static Post ToEntity(PostDto dto)
    {
        return new Post(dto.Id, dto.Title, dto.Body, dto.UserId);
    }

    public static PostDto ToDto(Post post, string username, List<CommentDto> comments)
    {
        return new PostDto(post.Id, post.Title, post.Body, post.UserId,
            username, comments);
    }
}

