using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        [HttpPost]
        public Task<IActionResult> UploadAsync(IFormFile file)
        {

        }
    }
}
