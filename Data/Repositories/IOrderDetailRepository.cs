using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Models;

namespace S8_Yostin_Arequipa.Data.Repositories;

public interface IOrderDetailRepository : IRepository<Orderdetail>
{
    // Métodos específicos para OrderDetail
    Task<IEnumerable<Orderdetail>> GetDetailsByOrderIdAsync(int orderId);
    Task<IEnumerable<ProductDetailInOrderDto>> GetProductDetailsByOrderIdAsync(int orderId);
    Task<int> GetTotalQuantityByOrderIdAsync(int orderId);
    Task<IEnumerable<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync();
    Task<IEnumerable<ClientWhoBoughtProductDto>> GetClientsWhoBoughtProductAsync(int productId);
}
