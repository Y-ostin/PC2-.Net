using S8_Yostin_Arequipa.DTOs;

namespace S8_Yostin_Arequipa.Services.Interfaces;

public interface IOrderDetailService
{
    Task<IEnumerable<OrderDetailDto>> GetAllOrderDetailsAsync();
    Task<OrderDetailDto?> GetOrderDetailByIdAsync(int id);
    Task<IEnumerable<OrderDetailDto>> GetOrderDetailsByOrderIdAsync(int orderId);
    Task<IEnumerable<ProductDetailInOrderDto>> GetProductDetailsByOrderIdAsync(int orderId);
    Task<OrderTotalQuantityDto> GetTotalQuantityByOrderIdAsync(int orderId);
    Task AddOrderDetailAsync(OrderDetailDto orderDetailDto);
    Task UpdateOrderDetailAsync(OrderDetailDto orderDetailDto);
    Task DeleteOrderDetailAsync(int id);
    Task<IEnumerable<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync();
    Task<IEnumerable<ClientWhoBoughtProductDto>> GetClientsWhoBoughtProductAsync(int productId);
}