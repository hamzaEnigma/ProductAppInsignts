using ProductSimple.DTOs;

namespace ProductSimple.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllAsync();
        Task<ProductResponseDto?> GetByIdAsync(int id);
        Task<ProductResponseDto> CreateAsync(CreateProductDto dto);
        ProductResponseDto GetDefaultProduct();
    }
}
