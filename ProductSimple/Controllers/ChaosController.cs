using Microsoft.AspNetCore.Mvc;

namespace ProductSimple.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChaosController : ControllerBase
    {

        private readonly ILogger<ChaosController> _logger;

        public ChaosController(ILogger<ChaosController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "LenteApi")]
        public IActionResult Get()
        {
            Console.WriteLine("Starting the program...");
            Thread.Sleep(4000);
            Console.WriteLine("Returning after 4 secs...");

            return Ok();
        }
    }
}
