using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }
        private readonly BloggieDbContext _bloggieDbContext;
        public AddModel( BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;   
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var newBlogPost = new BlogPost
            {
                Heading = AddBlogPostRequest.Heading,
                PageTitle = AddBlogPostRequest.PageTitle,
                Content = AddBlogPostRequest.Content,
                ShortDescription = AddBlogPostRequest.ShortDescription,
                FeaturedImageUrl = AddBlogPostRequest.FeaturedImageUrl,
                UrlHandle = AddBlogPostRequest.UrlHandle,
                PublishedDate = AddBlogPostRequest.PublishedDate,
                Author = AddBlogPostRequest.Author,
                Visible = AddBlogPostRequest.Visible
            };

            //Add to database using Context
           await _bloggieDbContext.BlogPosts.AddAsync(newBlogPost);

            //Save the chnages to DB
            await _bloggieDbContext.SaveChangesAsync();
            return RedirectToPage("/Admin/Blogs/List");
        }
    }
}
