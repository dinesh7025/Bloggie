using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Bloggie.web.Pages.Admin.Blogs
{
    [Authorize(Roles ="Admin")]
    public class AddModel : PageModel
    {
        private readonly IBlogPostrepository blogPostrepository;

        [BindProperty]
        public AddBlogPost AddBlogPostRequest { get; set; }
        [BindProperty]
        public IFormFile FeaturedImage { get; set; }
        [BindProperty]
        [Required]
        public string Tags { get; set; }


        public AddModel(IBlogPostrepository blogPostrepository)
        {
            this.blogPostrepository = blogPostrepository;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
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
                    Visible = AddBlogPostRequest.Visible,
                    Tags = new List<Tag>(Tags.Split(',').Select(t => new Tag() { Name = t.Trim() }))

                };

                //call from repo
                await blogPostrepository.AddAsync(newBlogPost);

                //Generic Message Display
                var notification = new Notification
                {
                    Message = $"New Blog Post for &apos;<strong>{newBlogPost.Heading}</strong>&apos; created successfully!",
                    Type = NotificationType.Success
                };
                //for message display after successfull add
                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Blogs/List");
            }
            return Page();
        }
    }
}
