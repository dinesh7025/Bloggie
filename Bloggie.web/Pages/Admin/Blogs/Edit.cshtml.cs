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
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IBlogPostrepository blogPostrepository;

        [BindProperty]
        public BlogPost BlogPost { get; set; }
        [BindProperty]
        public IFormFile FeaturedImage { get; set; }
        [BindProperty]
        public string Tags { get; set; }
        public EditModel(IBlogPostrepository blogPostrepository)
        {
            this.blogPostrepository = blogPostrepository;
        }
        public async Task OnGet(Guid id)
        {
            BlogPost = await blogPostrepository.GetAsync(id);

            if (BlogPost.Tags != null) {
                Tags = string.Join(",", BlogPost.Tags.Select(x=>x.Name));
            }

        }
        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                BlogPost.Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim()}));
                var updatedBlog = await blogPostrepository.UpdateAsync(BlogPost);

                ViewData["Notification"] = new Notification
                {
                    Message = $"<strong>&apos;{updatedBlog.Heading}&apos;</strong> Updated Successfully",
                    Type = NotificationType.Success
                };
            }
            catch (Exception)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = $"Something went wrong, Please Check the error log!",
                    Type = NotificationType.Error
                };

            }

            return Page();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await blogPostrepository.DeleteAsync(BlogPost.Id);
            if(deleted)
            {
                //Generic Message Display
                var notification = new Notification
                {
                    Message = $"Blog deleted successfully!",
                    Type = NotificationType.Info
                };
                //for message display after successfull add
                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/Admin/Blogs/List");
            }
                
            return Page();
        }
    }
}
