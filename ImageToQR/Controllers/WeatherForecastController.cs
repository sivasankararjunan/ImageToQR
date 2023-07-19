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

        [HttpPost()]
        public IActionResult SaveImage([FromForm] IFormFile objFile)
        {
            var output = _ImageService.SaveImage(objFile);
            return File(output, "image/png");
        }

        [HttpGet("{uid}")]
        public IActionResult GetImage([FromRoute] Guid uid)
        {
            var output = _ImageService.GetImage(uid);
            return File(output, "image/png");
        }
    }
}