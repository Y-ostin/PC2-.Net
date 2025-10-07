using Microsoft.EntityFrameworkCore;
using S8_Yostin_Arequipa.Models;

namespace S8_Yostin_Arequipa.Data.Repositories.Implementations;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _dbSet.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsWithPriceGreaterThanAsync(decimal price)
    {
        return await _dbSet.Where(p => p.Price > price).ToListAsync();
    }

    // 5
    public async Task<Product?> GetMostExpensiveProductAsync()
    {
        return await _dbSet.OrderByDescending(p => p.Price).FirstOrDefaultAsync();
    }

    // 7
    public async Task<decimal> GetAveragePriceAsync()
    {
        return await _dbSet.Select(p => p.Price).AverageAsync();
    }

    // 8
    public async Task<IEnumerable<Product>> GetProductsWithoutDescriptionAsync()
    {
        return await _dbSet
            .Where(p => string.IsNullOrEmpty(p.Description))
            .ToListAsync();
    }
}