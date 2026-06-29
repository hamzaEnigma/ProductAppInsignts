using Azure.Storage.Blobs;
using func_myapi_prod.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

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
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            _logger.LogInformation("Body reçu: {Body}", body);

            var product = JsonSerializer.Deserialize<ProductExportDto>(body,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var doc = new
            {
                productId = product.Id,
                productName = product.Name,
                exportedAt = DateTime.UtcNow,
                format = "JSON",
                status = "Success",
                documentReference = $"DOC-{DateTime.UtcNow:yyyy}-{product.Id:D4}"
            };

            // upload vers Blob storage
            var connectionString = Environment.GetEnvironmentVariable("ExportStorageConnection");
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient("exports");
            await containerClient.CreateIfNotExistsAsync();

            var blobName = $"{doc.documentReference}.json";
            var blobClient = containerClient.GetBlobClient(blobName);
            var docJson = JsonSerializer.Serialize(doc);

            await blobClient.UploadAsync(new BinaryData(docJson), overwrite: true);

            _logger.LogInformation("Document {BlobName} uploaded to Blob Storage", blobName);

            return new OkObjectResult(doc);
        }
    }
}
