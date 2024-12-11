using Bloggie.web.Models.Domains;

namespace Bloggie.web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikesForBlosPost(Guid blogPostId);
        Task AddLikeToBlogPost(Guid blogPostId, Guid UserId);
        Task<IEnumerable<BlogPostLike>> GetLikesForBlogPost(Guid blogPostId);
    }
}
