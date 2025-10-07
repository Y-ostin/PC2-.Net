using S8_Yostin_Arequipa.Models;

namespace S8_Yostin_Arequipa.Data.Repositories;

public interface IProductRepository : IRepository<Product>
{
    // Métodos específicos para Product
    Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
}