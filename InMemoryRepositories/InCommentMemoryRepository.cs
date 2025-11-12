using Entities; 
using Repository;

namespace InMemoryRepositories;

public class InCommentMemoryRepository : ICommentRepository
{
    private List<Comment> comments = new();
    
    public Task<Comment> AddAsync(string body, int postId, int userId)
    {
        
        int newId = comments.Any() 
            ? comments.Max(p => p.Id) + 1 : 1;
        Comment comment = new Comment(newId, body, postId, userId);
        comments.Add(comment);
        
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment existingComment = GetComment(comment.Id);
        comments.Remove(existingComment);
        
        comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment commentToRemove = GetComment(id);
        comments.Remove(commentToRemove);

        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        return Task.FromResult(GetComment(id));
    }

    public async Task<List<Comment>> GetMany()
    {
        return comments;
    }

    private Comment GetComment(int? id)
    {
        Comment? comment = comments.SingleOrDefault(p => p.Id == id);
        if (comment is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        return comment;
    }
}