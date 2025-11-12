using Entities;

namespace Repository;

public interface IPostRepository
{
    Task<Post> AddAsync(string title, string body, int userId); 
    Task UpdateAsync(Post post); 
    Task DeleteAsync(int id); 
    Task<Post> GetSingleAsync(int id); 
    IQueryable<Post> GetMany();
}