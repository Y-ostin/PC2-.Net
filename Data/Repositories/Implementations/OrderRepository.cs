using Microsoft.EntityFrameworkCore;
using S8_Yostin_Arequipa.Models;

namespace S8_Yostin_Arequipa.Data.Repositories.Implementations;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByClientIdAsync(int clientId)
    {
        return await _dbSet.Where(o => o.ClientId == clientId).ToListAsync();
    }
}