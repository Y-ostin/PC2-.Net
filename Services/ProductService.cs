using S8_Yostin_Arequipa.Data.Repositories;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Models;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(p => new ProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price
        });
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return null;
        return new ProductDto
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        };
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        var products = await _productRepository.GetByPriceRangeAsync(minPrice, maxPrice);
        return products.Select(p => new ProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price
        });
    }

    public async Task AddProductAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price
        };
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductDto productDto)
    {
        var product = await _productRepository.GetByIdAsync(productDto.ProductId);
        if (product != null)
        {
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            await _productRepository.UpdateAsync(product);
            await _productRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product != null)
        {
            await _productRepository.DeleteAsync(product);
            await _productRepository.SaveChangesAsync();
        }
    }
}