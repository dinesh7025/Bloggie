using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostCommentRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPostComment> AddAsync(BlogPostComment comment)
        {
            await bloggieDbContext.BlogPostComment.AddAsync(comment); 
            await bloggieDbContext.SaveChangesAsync();
            return comment;
        }

        public async Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostComment.Where(x => x.BlogPostid == blogPostId)
                .ToListAsync();
        }
    }
}
