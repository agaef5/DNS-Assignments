namespace DTOs;

public record PostDto(int Id, string Title, string Body, int UserId, string Username, List<CommentDto>? Comments);