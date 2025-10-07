namespace S8_Yostin_Arequipa.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
}