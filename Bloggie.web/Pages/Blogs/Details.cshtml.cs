using Bloggie.web.Models.Domains;
using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Blogs
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogPostrepository blogPostrepository;
        private readonly IBlogPostLikeRepository blogPostLikerepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBlogPostCommentRepository blogPostCommentRepository;

        public BlogPost BlogPost { get; set; }

        public List<BlogComment> BlogComments { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }
        [BindProperty]
        public Guid BlogPostId { get; set; }

        [BindProperty]
        public string Description { get; set; }
        public DetailsModel(IBlogPostrepository blogPostrepository, 
            IBlogPostLikeRepository blogPostLikerepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostrepository = blogPostrepository;
            this.blogPostLikerepository = blogPostLikerepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.blogPostCommentRepository = blogPostCommentRepository;
        }
        public async Task<IActionResult> OnGet(string urlHandle)
        {
            BlogPost = await blogPostrepository.GetAsync(urlHandle);
            if (BlogPost != null)
            {
                BlogPostId = BlogPost.Id;
                if (signInManager.IsSignedIn(User))
                {
                    var likes = await blogPostLikerepository.GetLikesForBlogPost(BlogPost.Id);
                    var userId = userManager.GetUserId(User);

                    Liked = likes.Any(x => x.UserId == Guid.Parse(userId));
                   

                    
                }
                await GetComments();
                TotalLikes = await blogPostLikerepository.GetTotalLikesForBlosPost(BlogPost.Id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(string urlHandle)
        {
            if(signInManager.IsSignedIn(User) && !string.IsNullOrWhiteSpace(Description)){
                
                var userId = userManager.GetUserId(User);
                
                var comment = new BlogPostComment
                {
                    BlogPostid = BlogPostId,
                    Description = Description,
                    UserId = Guid.Parse(userId),
                    DateAdded = DateTime.Now,
                };

                await blogPostCommentRepository.AddAsync(comment);
            }
            return RedirectToPage("/Blogs/Details", new { urlHandle = urlHandle});
            
        }

        private async Task GetComments()
        {
            var blogPostComments = await blogPostCommentRepository.GetAllAsync(BlogPost.Id);

            var blogPostCommentsViewModel = new List<BlogComment>();
            foreach(var blogPostComment in blogPostComments)
            {
                blogPostCommentsViewModel.Add(new BlogComment
                {
                    DateAdded = blogPostComment.DateAdded,
                    Description = blogPostComment.Description,
                    Username = (await userManager.FindByIdAsync(blogPostComment.UserId.ToString())).UserName,
                });
            }

            BlogComments = blogPostCommentsViewModel;
        }
    }
}
