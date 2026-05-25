using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;

namespace ProductSimple.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChaosController : ControllerBase
    {

        private readonly ILogger<ChaosController> _logger;
        private readonly TelemetryClient _telemetryClient;
        public ChaosController(ILogger<ChaosController> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        [HttpGet("/slowEnpoint")]
        public IActionResult Get()
        {
            Console.WriteLine("Starting the program...");
            Thread.Sleep(4000);
            Console.WriteLine("Returning after 4 secs...");

            return Ok();
        }

        [HttpGet("/sqlException")]
        public IActionResult GetBackendError()
        {
            try
            {
                throw new InvalidOperationException("the server doest respond");
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex, new Dictionary<string, string> {
                    ["chaos"] = "sql",
                    ["enpoint"] = "/chaos/sql",
                    ["impactedUser"] = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault() ?? "anonymous"
                });

                _logger.LogError(ex, "Chaos/error a levé une exception [{Type}]", "sql");

                return StatusCode(500, new
                {
                    ErrorName = "sql",
                    ErrorDescription = "InvalidOperationException",
                    message = ex.Message,
                    Error = ex.GetType().Name
                });
            }

        }
    }
}