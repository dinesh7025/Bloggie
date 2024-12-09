namespace Bloggie.web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikesForBlosPost(Guid blogPostId);
        Task AddLikeToBlogPost(Guid blogPostId, Guid UserId);
    }
}
