using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace EfcRepositories.Repositories;

public class EfcUserRepository(ForumContext context) : IUserRepository
{
    private readonly ForumContext _context = context;
    public async Task<User> AddAsync(string username, string password)
    {
        User user = new User(username, password);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        if (!(await _context.Users.AnyAsync(u => u.Id == user.Id)))
        {
            throw new Exception("User not found");
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        User? existing = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        if (existing == null)
        {
            throw new Exception($"User with id {id} not found");
        } 
        _context.Users.Remove(existing); 
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetSingleAsync(int id)
    {
        User? existing = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

        return existing;
    }

    public async Task<User?> GetSingleAsync(string username)
    {
        User? existing = await _context.Users.SingleOrDefaultAsync(u => u.Username.Equals(username));

        return existing;
    }

    public async Task<List<User>> GetMany()
    {
        return _context.Users.AsQueryable().ToList();
    }
}