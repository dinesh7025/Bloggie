using System.Net;
using Bloggie.web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository) {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
           var imageResult= await imageRepository.UploadAsync(file);
            if (imageResult == null) {
                return Problem("Something Went Wrong while uploading Image",null, (int)HttpStatusCode.InternalServerError);
            }
            return Json(new { Link = imageResult });

        }
    }
}
