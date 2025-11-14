using Entities;

namespace Repository;

public interface IUserRepository
{
    Task<User> AddAsync(string username, string password); 
    Task UpdateAsync(User user); 
    Task DeleteAsync(int id); 
    Task<User?> GetSingleAsync(int id);
    Task<User?> GetSingleAsync(string username);
    Task<List<User>> GetMany(); 
}