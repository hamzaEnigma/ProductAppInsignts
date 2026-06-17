using Microsoft.AspNetCore.Mvc;
using ProductSimple.DTOs;
using ProductSimple.Services;

namespace ProductSimple.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly string?  _functionUrl;
        private readonly IProductService _serviceProduct;
        public ProductController(IConfiguration config, IProductService service)
        {
            _functionUrl = config["AzureFunction:AzFunctionExportProduct"];
            _serviceProduct = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _serviceProduct.GetAllAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _serviceProduct.GetByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var created = await _serviceProduct.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpGet("Export/{id:int}")]
        public async Task<IActionResult> Export(int id = 0)
        {
            HttpClient client = new HttpClient();
            var product = _serviceProduct.GetDefaultProduct();
            var response = await client.PostAsJsonAsync(_functionUrl,product);
            if (!response.IsSuccessStatusCode)
            {
                return new ObjectResult("erreur dans la fonction:"+response.StatusCode);
            }
            var result = await response.Content.ReadAsStringAsync();
            return new ObjectResult(result);
        }
    }
}
