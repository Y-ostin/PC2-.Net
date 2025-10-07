using S8_Yostin_Arequipa.DTOs;

namespace S8_Yostin_Arequipa.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<ProductDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    Task<IEnumerable<ProductDto>> GetProductsWithPriceGreaterThanAsync(decimal price);
    Task AddProductAsync(ProductDto productDto);
    Task UpdateProductAsync(ProductDto productDto);
    Task DeleteProductAsync(int id);
    Task<decimal> GetAveragePriceAsync();
    Task<IEnumerable<ProductDto>> GetProductsWithoutDescriptionAsync();
}