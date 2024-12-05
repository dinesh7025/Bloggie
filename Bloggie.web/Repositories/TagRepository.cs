using Bloggie.web.Data;
using Bloggie.web.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           var tags = await bloggieDbContext.Tags.ToListAsync();
            return tags.DistinctBy(t => t.Name.ToLower());
        }
    }
}
