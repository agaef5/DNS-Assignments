using Entities;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace EfcRepositories.Repositories;

public class EfcPostRepository(ForumContext context) : IPostRepository
{
    private readonly ForumContext _context = context;
    public async Task<Post> AddAsync(string title, string body, int userId)
    {
        Post post = new Post(title, body, userId);
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task UpdateAsync(Post post)
    {
        if (!(await _context.Posts.AnyAsync(p => p.Id == post.Id)))
        {
            throw new Exception("Post not found");
        }

        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Post? post = await context.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if(post == null){
            throw new Exception("Post not found");
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
        Post? post = await context.Posts.SingleOrDefaultAsync(p => p.Id == id);
        if(post == null){
            throw new Exception("Post not found");
        }

        return post;
    }

    public async Task<List<Post>> GetMany()
    {
        return _context.Posts.AsQueryable().ToList();
    }
}