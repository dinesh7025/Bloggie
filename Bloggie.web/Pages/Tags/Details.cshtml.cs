using Bloggie.web.Models.Domains;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostrepository blogPostrepository;

        public List<BlogPost> Blogs { get; set; }
        public DetailsModel(IBlogPostrepository blogPostrepository)
        {
            this.blogPostrepository = blogPostrepository;
        }
        public async Task<IActionResult> OnGet(string tagName)
        {
            Blogs = (await blogPostrepository.GetAllAsync(tagName)).ToList();
            return Page();
        }
    }
}
