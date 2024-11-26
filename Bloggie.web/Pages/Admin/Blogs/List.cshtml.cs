using System.Runtime.CompilerServices;
using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly BloggieDbContext _bloggieDbContext;
        public List<BlogPost> BlogPosts { get; set; }
        public ListModel(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }
        public async Task OnGet()
        {
            BlogPosts = await _bloggieDbContext.BlogPosts.ToListAsync();
        }
    }
}
