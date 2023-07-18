using ImageToQR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ImageToQR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {



        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IImageService _ImageService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IImageService imageService)
        {
            _logger = logger;
            _ImageService = imageService;

        }

        [HttpPost(Name = "Save")]
        public IActionResult SaveImage([FromForm] IFormFile objFile)
        {
            var output = _ImageService.SaveImage(objFile);
            return Ok();
        }

        [HttpGet(Name = "Get/{ImageUid}")]
        public IActionResult GetImage(string ImageUid)
        {
            return Ok();

        }
    }
}