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
    
    public async Task<Comment> AddAsync(Comment comment)
    {
        List<Comment> comments = await ReadCommentsAsync();
        
        int? maxId = comments.Count > 0 ? comments.Max(c => c.id) : 1;
        comment.id = maxId + 1;
        comments.Add(comment);

        await WriteCommentsAsync(comments);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        List<Comment> comments = await ReadCommentsAsync();
        Comment existingComment = GetComment(comments, comment.id);

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

    public IQueryable<Comment> GetMany()
    {
        var comments = ReadCommentsAsync().Result;
        return comments.AsQueryable();
    }
    
    private static Comment GetComment(List<Comment> comments, int? id)
    {
        var comment = comments.SingleOrDefault(p => p.id == id);
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