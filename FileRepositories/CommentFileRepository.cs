using System.Text.Json;
using Entities;
using Repository;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string _filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");  
        }
    }
    
    public async Task<Comment> AddAsync(string body, int postId, int userId)
    {
        List<Comment> comments = await ReadCommentsAsync();
        
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) + 1 : 1;

        Comment comment = new Comment(maxId, body, postId, userId);
        comments.Add(comment);

        await WriteCommentsAsync(comments);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        List<Comment> comments = await ReadCommentsAsync();
        Comment existingComment = GetComment(comments, comment.Id);

        comments.Remove(existingComment);
        comments.Add(comment);

        await WriteCommentsAsync(comments);
    }

    public async Task DeleteAsync(int id)
    {
        List<Comment> comments = await ReadCommentsAsync();
        Comment existingComment = GetComment(comments, id);

        comments.Remove(existingComment);

        await WriteCommentsAsync(comments);
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        List<Comment> comments = await ReadCommentsAsync();
        Comment existingComment = GetComment(comments, id);

        return existingComment;
    }

    public async Task<List<Comment>> GetMany()
    {
        var comments = await ReadCommentsAsync();
        return comments;
    }
    
    private static Comment GetComment(List<Comment> comments, int? id)
    {
        var comment = comments.SingleOrDefault(p => p.Id == id);
        return comment ?? throw new InvalidOperationException($"Comment with ID '{id}' not found");
    }
    
    private async Task<List<Comment>> ReadCommentsAsync()
    {
        var commentsAsJson = await File.ReadAllTextAsync(_filePath);
        var comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments;
    }

    private async Task WriteCommentsAsync(List<Comment> comments)
    {
        var commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(_filePath, commentsAsJson);
    }
}