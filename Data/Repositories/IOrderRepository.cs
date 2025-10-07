using S8_Yostin_Arequipa.Models;

namespace S8_Yostin_Arequipa.Data.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    // Métodos específicos para Order
    Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId);
}