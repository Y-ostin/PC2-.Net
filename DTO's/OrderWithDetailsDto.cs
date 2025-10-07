namespace S8_Yostin_Arequipa.DTOs;

public class OrderWithDetailsDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
