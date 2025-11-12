using Entities;

namespace Repository;

public interface ICommentRepository 
{ 
    Task<Comment> AddAsync(string body, int postId, int userId); 
    Task UpdateAsync(Comment comment); 
    Task DeleteAsync(int id); 
    Task<Comment> GetSingleAsync(int id); 
    Task<List<Comment>> GetMany(); 
}