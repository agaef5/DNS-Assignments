using System.Runtime.InteropServices.Marshalling;
using Entities;
using Repository;

namespace InMemoryRepositories;

public class InPostMemoryRepository : IPostRepository
{
    private List<Post> posts = new ();
    
    public Task<Post> AddAsync(string title, string body, int userId)
    {
        int newId = posts.Any()? posts.Max(p => p.Id) + 1 : 1;
        Post post = new Post(newId, title, body, userId);
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post) 
    { 
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost is null)
        {
            throw new InvalidOperationException($"Post with ID '{post.Id}' not found");
        } 
        
        posts.Remove(existingPost);
        posts.Add(post);
        
        return Task.CompletedTask; 
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id  == id);
        if (postToRemove is null)
        {
            throw new InvalidOperationException( $"Post with ID '{id}' not found");
        } 
        posts.Remove(postToRemove); 
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? postToGet = posts.SingleOrDefault(p => p.Id  == id);
        if (postToGet is null)
        {
            throw new InvalidOperationException( $"Post with ID '{id}' not found");
        } 
        return Task.FromResult(postToGet);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}