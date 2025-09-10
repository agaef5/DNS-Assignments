using System;
using System.Threading.Tasks;
using Entities;

namespace Repository
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user); 
        Task UpdateAsync(User user); 
        Task DeleteAsync(int id); 
        Task<User> GetSingleAsync(int id); 
        IEquatable<Post> GetMany(); 
    }
}