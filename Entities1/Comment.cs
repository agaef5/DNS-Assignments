namespace Entities1
{
    public class Comment
    {
        public int id { get; set; }
        public string body { get; set; }
        public int postId { get; set; }
        public int userId { get; set; }
    }
}