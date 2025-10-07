using S8_Yostin_Arequipa.DTOs;

namespace S8_Yostin_Arequipa.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    Task<OrderDto?> GetOrderByIdAsync(int id);
    Task<IEnumerable<OrderDto>> GetOrdersByClientIdAsync(int clientId);
    Task AddOrderAsync(OrderDto orderDto);
    Task UpdateOrderAsync(OrderDto orderDto);
    Task DeleteOrderAsync(int id);
}