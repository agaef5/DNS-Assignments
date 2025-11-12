namespace DTOs.Comments;

public record CommentDto(int Id, string Body, int PostId, int UserId, string Username);