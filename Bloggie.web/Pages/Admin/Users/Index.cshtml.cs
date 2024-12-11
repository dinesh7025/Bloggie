using Bloggie.web.Models.ViewModels;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserrepository userRepository;
        public List<User> Users { get; set; }
        [BindProperty]
        public AddUser AddUserRequest { get; set; }

        public IndexModel(IUserrepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<IActionResult> OnGet()
        {
            var users = await userRepository.GetAllAsync();
            Users = new List<User>();

            foreach (var user in users) {
                Users.Add(new User()
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var identityUser = new IdentityUser
            {
                UserName = AddUserRequest.Username,
                Email = AddUserRequest.Email,
            };

            var roles = new List<string> { "User" };
            if (AddUserRequest.AdminCheckBox)
            {
                roles.Add("Admin");
            }

            var result = await userRepository.Add(identityUser, AddUserRequest.Password, roles);

            if (result)
            {
                return RedirectToPage("/Admin/Users/Index");
            }
             return Page();
        }
    }
}
