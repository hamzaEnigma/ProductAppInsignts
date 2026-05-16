using Microsoft.AspNetCore.Mvc;
using ProductSimple.Infrastructure;
using System.Reflection;

namespace ProductSimple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
        {
            var assembly = Assembly.GetExecutingAssembly();

            var result = new
            {
                Version = assembly.GetName().Version,
                BuiltAt = BuildInfo.builtAt,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            };
            return Ok(result);
        }
    }
}
