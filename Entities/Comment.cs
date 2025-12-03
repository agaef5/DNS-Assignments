using DTOs.Comments;

namespace Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }


        public Comment(int id, string body, int postId, int userId)
        {
            this.Id = id;
            this.Body = body;
            this.PostId = postId;
            this.UserId = userId;
        }
        
        private Comment(){} //for EFC
        
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