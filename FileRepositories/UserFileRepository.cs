using System.Text.Json;
using Entities;
using Repository;

namespace FileRepositories;

public class UserFileRepository : IUserRepository
{
    private readonly string _filePath = "users.json";

    public UserFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");  
        } 
    }
    
    public async Task<User> AddAsync(User user)
    {
        List<User> users =  GetMany().ToList();
        
        int? newId = users.Count > 0 ? users.Last().Id + 1 : 1;
        user.Id = newId;
        
        users.Add(user);
        await WriteUsersAsync(users);

        return user;
    }

    public async Task UpdateAsync(User user)
    {
        List<User> users =  GetMany().ToList();

        User existingUser = GetUser(users, user.Id);
        users.Remove(existingUser);
        users.Add(user);
        
        await WriteUsersAsync(users);
    }

    public async Task DeleteAsync(int id)
    {        
        List<User> users =  GetMany().ToList();

        User existingUser = GetUser(users, id);
        users.Remove(existingUser);
        
        await WriteUsersAsync(users);
    }

    public async Task<User> GetSingleAsync(int id)
    {
        List<User> users =  GetMany().ToList();

        User existingUser = GetUser(users, id);

        return existingUser;
    }

    public IQueryable<User> GetMany()
    {
        var users = ReadUsersAsync().Result;
        return users.AsQueryable();
    }
    
    private static User GetUser(List<User> users, int? id)
    {
        var user = users.SingleOrDefault(p => p.Id == id);
        return user ?? throw new InvalidOperationException($"User with ID '{id}' not found");
    }
    
    private async Task<List<User>> ReadUsersAsync()
    {
        var userAsJson = await File.ReadAllTextAsync(_filePath);
        var users = JsonSerializer.Deserialize<List<User>>(userAsJson)!;
        return users;
    }

    private async Task WriteUsersAsync(List<User> users)
    
    {
        var usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(_filePath, usersAsJson);
    }
}