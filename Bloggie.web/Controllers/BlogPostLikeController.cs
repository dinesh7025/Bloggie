using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostLikeController : Controller
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] AddBlogPostLikeRequest addBlogPostLikeRequest)
        {
            await blogPostLikeRepository.AddLikeToBlogPost(addBlogPostLikeRequest.BlogPostId,
                addBlogPostLikeRequest.UserId);
            return Ok();
        }
        [HttpGet]
        [Route("{blogPostid:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikes([FromRoute] Guid blogPostId)
        {
            var totalLikes = await blogPostLikeRepository.GetTotalLikesForBlosPost(blogPostId); 
            return Ok(totalLikes);
        }
    }
}
