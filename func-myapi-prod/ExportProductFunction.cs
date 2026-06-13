using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace func_myapi_prod
{
    public class ExportProductFunction
    {
        private readonly ILogger<ExportProductFunction> _logger;

        public ExportProductFunction(ILogger<ExportProductFunction> logger)
        {
            _logger = logger;
        }

        [Function("ExportProductFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to my project");
        }
    }
}
