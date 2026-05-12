using Microsoft.EntityFrameworkCore;
using ProductSimple.Domain;

namespace ProductSimple.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync() =>
               await context.Products.ToListAsync();
        public async Task<Product?> GetByIdAsync(int id) =>
                await context.Products.FindAsync(id);

        public async Task<Product> CreateAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }
    }
}
