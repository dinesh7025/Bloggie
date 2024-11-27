using System.Runtime.CompilerServices;
using System.Text.Json;
using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Pages.Admin.Blogs
{
    public class ListModel : PageModel
    {
        private readonly IBlogPostrepository blogPostrepository;

        public List<BlogPost> BlogPosts { get; set; }
        public ListModel(IBlogPostrepository blogPostrepository)
        {
            this.blogPostrepository = blogPostrepository;
        }
        public async Task OnGet()
        {
            //check for any notification before loading...
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            BlogPosts = (await blogPostrepository.GetAllAsync())?.ToList();
        }
    }
}
