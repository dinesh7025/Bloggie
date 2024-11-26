using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class EditModel : PageModel
    {

        private readonly BloggieDbContext _bloggieDbContext;
        [BindProperty]
        public BlogPost BlogPost { get; set; }
        public EditModel(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }
        public async Task OnGet(Guid id)
        {
            BlogPost = await _bloggieDbContext.BlogPosts.FindAsync(id);
        }
        public async Task<IActionResult> OnPostEdit()
        {
            var existingBlogPost = await _bloggieDbContext.BlogPosts.FindAsync(BlogPost.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = BlogPost.Heading;
                existingBlogPost.Content = BlogPost.Content;
                existingBlogPost.PageTitle = BlogPost.PageTitle;
                existingBlogPost.ShortDescription = BlogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = BlogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = BlogPost.UrlHandle;
                existingBlogPost.Author = BlogPost.Author;
                existingBlogPost.PublishedDate = BlogPost.PublishedDate;
                existingBlogPost.Visible = BlogPost.Visible;
            }

            await _bloggieDbContext.SaveChangesAsync();

            return RedirectToPage("/Admin/Blogs/List");
        }
        public async Task<IActionResult> OnPostDelete()
        {
            var existingBlogPost = await _bloggieDbContext.BlogPosts.FindAsync(BlogPost.Id);
            if (existingBlogPost != null)
            {
                _bloggieDbContext.BlogPosts.Remove(existingBlogPost);
                await _bloggieDbContext.SaveChangesAsync();
                return RedirectToPage("/Admin/Blogs/List");
            }
            return Page();
        }
    }
}
