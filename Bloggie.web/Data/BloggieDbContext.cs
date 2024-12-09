using Bloggie.web.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions<BloggieDbContext> options): base(options)
        {
        }


        //Adding Db set Properties
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogPostLike> BlogPostLike { get; set; }
    }
}
