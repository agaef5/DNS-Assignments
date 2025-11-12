using DTOs.Comments;

namespace Entities
{
    public class Comment(int id, string body, int postId, int userId)
    {
        public int Id { get; set; } = id;
        public string Body { get; set; } = body;
        public int PostId { get; set; } = postId;
        public int UserId { get; set; } = userId;
        
        public static Comment ToEntity(CommentDto dto )
        {
            return new Comment(dto.Id, dto.Body, dto.PostId, dto.UserId);
        }

        public static CommentDto ToDto(Comment comment)
        {
            return new CommentDto(comment.Id, comment.Body, comment.PostId,
                comment.UserId, "");
        }

        public static CommentDto ToDto(Comment comment, string username)
        {
            return new CommentDto(comment.Id, comment.Body, comment.PostId,
                comment.UserId, username);
        }
    }
    

}