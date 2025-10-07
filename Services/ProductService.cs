using S8_Yostin_Arequipa.Data.UnitOfWork;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Models;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
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
        var product = await _unitOfWork.Products.GetByIdAsync(id);
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
        var products = await _unitOfWork.Products.GetByPriceRangeAsync(minPrice, maxPrice);
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
        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(ProductDto productDto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(productDto.ProductId);
        if (product != null)
        {
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            await _unitOfWork.Products.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ProductDto>> GetProductsWithPriceGreaterThanAsync(decimal price)
    {
        var products = await _unitOfWork.Products.GetProductsWithPriceGreaterThanAsync(price);
        return products.Select(p => new ProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price
        });
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product != null)
        {
            await _unitOfWork.Products.DeleteAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    // 7: Obtener el promedio de precio de los productos
    public async Task<decimal> GetAveragePriceAsync()
    {
        return await _unitOfWork.Products.GetAveragePriceAsync();
    }

    // 8: Obtener todos los productos sin descripción
    public async Task<IEnumerable<ProductDto>> GetProductsWithoutDescriptionAsync()
    {
        var products = await _unitOfWork.Products.GetProductsWithoutDescriptionAsync();
        return products.Select(p => new ProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price
        });
    }
}