using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class BlogPostRepository : IBlogPostrepository
    {
        private readonly BloggieDbContext _bloggieDbContext;
        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            //Add to database using Context
            await _bloggieDbContext.BlogPosts.AddAsync(blogPost);

            //Save the chnages to DB
            await _bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _bloggieDbContext.BlogPosts.FindAsync(id);
            if (existingBlogPost != null)
            {
                _bloggieDbContext.BlogPosts.Remove(existingBlogPost);
                await _bloggieDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _bloggieDbContext.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
            return await _bloggieDbContext.BlogPosts.FindAsync(id);
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _bloggieDbContext.BlogPosts.FindAsync(blogPost.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Visible = blogPost.Visible;
            }

            await _bloggieDbContext.SaveChangesAsync();
            return existingBlogPost;
        }
    }
}
