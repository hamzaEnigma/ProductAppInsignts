using Microsoft.AspNetCore.Mvc;
using ProductSimple.Infrastructure;

namespace ProductSimple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
        {
            var result = new
            {
                Version = "1.0.0",
                BuiltAt = BuildInfo.builtAt,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            };
            return Ok(result);
        }
    }
}
