using Microsoft.EntityFrameworkCore;
using S8_Yostin_Arequipa.Models;

namespace S8_Yostin_Arequipa.Data.Repositories.Implementations;

public class OrderDetailRepository : Repository<Orderdetail>, IOrderDetailRepository
{
    public OrderDetailRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Orderdetail>> GetDetailsByOrderIdAsync(int orderId)
    {
        return await _dbSet.Where(od => od.OrderId == orderId).ToListAsync();
    }
}