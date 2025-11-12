namespace Entities
{
    public class Comment(int id, string body, int postId, int userId)
    {
        public int Id { get; set; } = id;
        public string Body { get; set; } = body;
        public int PostId { get; set; } = postId;
        public int UserId { get; set; } = userId;
    }
}