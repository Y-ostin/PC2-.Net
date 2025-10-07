using S8_Yostin_Arequipa.DTOs;

namespace S8_Yostin_Arequipa.Services.Interfaces;

public interface IOrderDetailService
{
    Task<IEnumerable<OrderDetailDto>> GetAllOrderDetailsAsync();
    Task<OrderDetailDto?> GetOrderDetailByIdAsync(int id);
    Task<IEnumerable<OrderDetailDto>> GetOrderDetailsByOrderIdAsync(int orderId);
    Task AddOrderDetailAsync(OrderDetailDto orderDetailDto);
    Task UpdateOrderDetailAsync(OrderDetailDto orderDetailDto);
    Task DeleteOrderDetailAsync(int id);
}