using Microsoft.AspNetCore.Identity;

namespace Bloggie.web.Repositories
{
    public interface IUserrepository
    {
        Task<IEnumerable<IdentityUser>> GetAllAsync();
        Task<bool> Add(IdentityUser identityUser, string password, List<string> roles );
        Task Delete(Guid id);
    }
}
