using Bloggie.web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        [BindProperty]
        public Login LoginViewModel { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
           var signInresult =  await signInManager.PasswordSignInAsync(
                LoginViewModel.Username, LoginViewModel.Password, false, false);
            if (signInresult.Succeeded)
            {
                return RedirectToPage("Index");
            }
            else 
            {
                ViewData["Notification"] = new Notification
                {
                    Type = NotificationType.Error,
                    Message = "Unable to Login. Please check your credentials!"
                };
                return Page();
            }
            
        }
    }
}
