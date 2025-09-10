using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Repository
{
    public interface ICommentRepository 
    { 
        Task<Comment> AddAsync(Comment comment); 
        Task UpdateAsync(Comment comment); 
        Task DeleteAsync(int id); 
        Task<Comment> GetSingleAsync(int id); 
        IQueryable<Comment> GetMany(); 
    }
}