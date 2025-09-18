using Entities; 
using Repository;

namespace InMemoryRepositories;

public class InCommentMemoryRepository : ICommentRepository
{
    private List<Comment> comments = new();
    
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.id = comments.Any() 
            ? comments.Max(p => p.id) + 1 : 1; comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        if (comment.id == null)
        {
            Console.WriteLine("Comment does not have id.");
            return Task.CompletedTask;
        }
        Comment existingComment = getComment(comment.id);
        comments.Remove(existingComment);
        
        comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment commentToRemove = getComment(id);
        comments.Remove(commentToRemove);

        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        return Task.FromResult(getComment(id));
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }

    private Comment getComment(int? id)
    {
        Comment? comment = comments.SingleOrDefault(p => p.id == id);
        if (comment is null)
        {
            throw new InvalidOperationException($"Post with ID '{id}' not found");
        }
        return comment;
    }
}