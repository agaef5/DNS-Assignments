using DTOs.Comments;

namespace ClientApp.Services.Comment;

public interface ICommentService
{
    Task<CommentDto> CreateCommentAsync(CreateCommentDto commentDto);
}