using Entities1;
using Repository1;

namespace InMemoryRepositories1;

public class InUserMemoryRepository : IUserRepository
{
    private List<User> users;
    
    public Task<User> AddAsync(User user)
    {
        user.id = users.Any() ? users.Max(u => u.id) + 1 : 1;
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = getUser(user.id);

        users.Remove(existingUser);
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = getUser(id);

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        return Task.FromResult(getUser(id));
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }

    private User getUser(int id)
    {
        User? user = users.SingleOrDefault(u => u.id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }
        return user;
    }
}