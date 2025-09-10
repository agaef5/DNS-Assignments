using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Repository;

namespace InMemoryRepositories
{
    public class PostInMemoryRepository : IPostRepository
    {
        public List<Post> posts;
        public Task<Post> AddAsync(Post post)
        {
            post.id = posts.Any() ? posts.Max(p => p.id) + 1 : 1;
            posts.Add(post);
            return Task.FromResult(post);
        }

        public Task UpdateAsync(Post post)
        {
            Post? existingPost = posts.SingleOrDefault(p => p.id == post.id);
            if (existingPost is null)
            {
                throw new InvalidOperationException($"Post with ID {post.id} not found");
            }

            posts.Remove(existingPost);
            posts.Add(post);
            
            return Task.CompletedTask
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Post> GetSingleAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<Post> GetMany()
        {
            throw new System.NotImplementedException();
        }
    }
}