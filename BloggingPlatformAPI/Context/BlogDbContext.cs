using BloggingPlatformAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatformAPI.Context
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
    }
}