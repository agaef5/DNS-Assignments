using Entities1;

namespace Repository1;

public interface IUserRepository
{
    Task<User> AddAsync(User user); 
    Task UpdateAsync(User user); 
    Task DeleteAsync(int id); 
    Task<User> GetSingleAsync(int id); 
    IQueryable<User> GetMany(); 
}