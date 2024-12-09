
using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task AddLikeToBlogPost(Guid blogPostId, Guid UserId)
        {
            var like = new BlogPostLike
            {
                Id = Guid.NewGuid(),
                BlogPostid = blogPostId,
                UserId = UserId
            };

            await bloggieDbContext.BlogPostLike.AddAsync(like);
            await bloggieDbContext.SaveChangesAsync();
        }

        public async Task<int> GetTotalLikesForBlosPost(Guid blogPostId)
        {
            return await bloggieDbContext.BlogPostLike
                 .CountAsync(x => x.BlogPostid == blogPostId);
            
        }
    }
}
