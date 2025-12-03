using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class ForumContext : DbContext
{
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                var path = @"C:\Users\agafu\RiderProjects\DNPCourseAssignment\EfcRepositories\forum.db";
                Console.WriteLine($"Using DB at: {path}");
                optionsBuilder.UseSqlite($"Data Source={path}");
        }
}