namespace DTOs.Comments;

public record CreateCommentDto(string Body, int PostId, int UserId);