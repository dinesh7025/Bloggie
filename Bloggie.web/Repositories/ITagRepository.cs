using Bloggie.web.Models.Domains;

namespace Bloggie.web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
    }
}
