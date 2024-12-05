using Bloggie.web.Models.Domains;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Blogs
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostrepository blogPostrepository;

        public BlogPost BlogPost { get; set; }
        public DetailsModel(IBlogPostrepository blogPostrepository)
        {
            this.blogPostrepository = blogPostrepository;
        }
        public async Task<IActionResult> OnGet(string urlHandle)
        {
            BlogPost = await blogPostrepository.GetAsync(urlHandle);
            return Page();
        }
    }
}
