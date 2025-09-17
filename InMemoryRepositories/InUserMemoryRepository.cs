using Entities;
using Repository;

namespace InMemoryRepositories;

public class InUserMemoryRepository : IUserRepository
{
    private readonly List<User> _users = new();
    
    public Task<User> AddAsync(User user)
    {
        user.Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1;
        _users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = GetUser(user.Id);

        _users.Remove(existingUser);
        _users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = GetUser(id);

        _users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        return Task.FromResult(GetUser(id));
    }

    public IQueryable<User> GetMany()
    {
        return _users.AsQueryable();
    }

    private User GetUser(int? id)
    {
        User? user = _users.SingleOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return user;
    }
}