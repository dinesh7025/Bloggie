namespace Bloggie.web.Models.Domains
{
    public class BlogPostLike
    {
        public Guid Id { get; set; }
        public Guid BlogPostid { get; set; }    
        public Guid UserId { get; set; }
    }
}
