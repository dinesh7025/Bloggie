using Bloggie.web.Models.Domains;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Blogs
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostrepository blogPostrepository;
        private readonly IBlogPostLikeRepository blogPostLikerepository;

        public BlogPost BlogPost { get; set; }
        public int TotalLikes { get; set; }
        public DetailsModel(IBlogPostrepository blogPostrepository, 
            IBlogPostLikeRepository blogPostLikerepository)
        {
            this.blogPostrepository = blogPostrepository;
            this.blogPostLikerepository = blogPostLikerepository;
        }
        public async Task<IActionResult> OnGet(string urlHandle)
        {
            BlogPost = await blogPostrepository.GetAsync(urlHandle);
            if (BlogPost != null)
            {
                TotalLikes = await blogPostLikerepository.GetTotalLikesForBlosPost(BlogPost.Id);
            }
            return Page();
        }
    }
}
