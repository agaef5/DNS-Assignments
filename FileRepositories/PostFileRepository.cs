using System.Text.Json;
using Entities;
using Repository;

namespace FileRepositories;

public class PostFileRepository : IPostRepository
{
    private readonly string _filePath = "posts.json";

    public PostFileRepository()
    {
        if (!File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "[]");
        }
    }
    
    public async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = await ReadPostsAsync();

        int? newId = posts.Count > 0 ? posts.Last().id + 1 : 1;
        post.id = newId;
        
        posts.Add(post);

        await WritePostsAsync(posts);
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        List<Post> posts = await ReadPostsAsync();

        Post existingPost = GetPost(posts, post.id);
        posts.Remove(existingPost);
        posts.Add(post);

        await WritePostsAsync(posts);
    }

    public async Task DeleteAsync(int id)
    {
        List<Post> posts = await ReadPostsAsync();

        Post existingPost = GetPost(posts, id);
        posts.Remove(existingPost);
        
        await WritePostsAsync(posts);
        
    }

    public async Task<Post> GetSingleAsync(int id)
    {         
        List<Post> posts = await ReadPostsAsync();
        return GetPost(posts, id);
    }

    public IQueryable<Post> GetMany()
    {
        List<Post> posts =  ReadPostsAsync().Result;
        return posts.AsQueryable();
    }
    
    private static Post GetPost(List<Post> posts, int? id)
    {
        var post = posts.SingleOrDefault(p => p.id == id);
        return post ?? throw new InvalidOperationException($"Post with ID '{id}' not found");
    }
    
    private async Task<List<Post>> ReadPostsAsync()
    {
        var postsAsJson = await File.ReadAllTextAsync(_filePath);
        var posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson)!;
        return posts;
    }

    private async Task WritePostsAsync(List<Post> posts)
    {
        var postsAsJson = JsonSerializer.Serialize(posts);
        await File.WriteAllTextAsync(_filePath, postsAsJson);
    }
}