namespace DTOs;

public record CreateCommentDto(string Body, int PostId, int UserId);