using Bloggie.web.Models.Domains;

namespace Bloggie.web.Repositories
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostComment> AddAsync(BlogPostComment comment);
        Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId);
    }
}
