using Microsoft.AspNetCore.Mvc;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet("price-range")]
    public async Task<IActionResult> GetByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
    {
        var products = await _productService.GetProductsByPriceRangeAsync(minPrice, maxPrice);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _productService.AddProductAsync(productDto);
        return CreatedAtAction(nameof(GetById), new { id = productDto.ProductId }, productDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductDto productDto)
    {
        if (id != productDto.ProductId) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _productService.UpdateProductAsync(productDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}