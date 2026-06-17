using ProductSimple.Domain;
using ProductSimple.DTOs;
using ProductSimple.Repositories;

namespace ProductSimple.Services
{
    public class ProductService(IProductRepository repository) : IProductService

    {
        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var products = await repository.GetAllAsync();
            return products.Select(ToDto);
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await repository.GetByIdAsync(id);
            return product is null ? null : ToDto(product);
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product { Name = dto.Name, Price = dto.Price, Description = dto.Description };
            var created = await repository.CreateAsync(product);
            return ToDto(created);
        }

        private static ProductResponseDto ToDto(Product p) =>
            new(p.Id, p.Name, p.Price, p.Description, p.CreatedAt);

        public ProductResponseDto GetDefaultProduct()
        {
            var product = new Product { Name = "dto.Name", Description = "dto.Description" };
            return ToDto(product);
        }
    }
}
