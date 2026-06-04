using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
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

        [HttpGet("slowEnpoint")]
        public async Task<IActionResult> Get()
        {
            var rand = new Random();
            var timeWaiting = rand.Next(3000, 6000);

            var impactedUser = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault() ?? "anonymous";

            _telemetryClient.TrackEvent("slowEndpoint",
                new Dictionary<string, string> {
                    ["endpoint"] = "/slowEnpoint",
                    ["impactedUser"] = impactedUser,
                    ["tempsAttendu"] = timeWaiting.ToString()
                });

            await Task.Delay(timeWaiting);

            return StatusCode(200, new Dictionary<string, string>
            {
                ["endpoint"] = "/slowEnpoint",
                ["impactedUser"] = impactedUser,
                ["tempsAttendu"] = timeWaiting.ToString()
            });
        }

        [HttpGet("sqlException")]
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