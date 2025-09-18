namespace Entities
{
    public class Comment(int? id, string body, int postId, int userId)
    {
        public int? id { get; set; } = id;
        public string body { get; set; } = body;
        public int postId { get; set; } = postId;
        public int userId { get; set; } = userId;
    }
}