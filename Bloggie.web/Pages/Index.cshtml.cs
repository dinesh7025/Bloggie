using Bloggie.web.Models.Domains;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogPostrepository blogPostrepository;
        public List<BlogPost> Blogs { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IBlogPostrepository blogPostrepository)
        {
            _logger = logger;
            this.blogPostrepository = blogPostrepository;
        }

        public IBlogPostrepository BlogPostrepository { get; }

        public async Task<IActionResult> OnGet()
        {
            Blogs = (await blogPostrepository.GetAllAsync()).ToList();
            return Page();
        }
    }
}
