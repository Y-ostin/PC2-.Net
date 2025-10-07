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
}