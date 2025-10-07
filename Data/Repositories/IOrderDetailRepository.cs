using S8_Yostin_Arequipa.Models;

namespace S8_Yostin_Arequipa.Data.Repositories;

public interface IOrderDetailRepository : IRepository<Orderdetail>
{
    // Métodos específicos para OrderDetail
    Task<IEnumerable<Orderdetail>> GetDetailsByOrderIdAsync(int orderId);
}