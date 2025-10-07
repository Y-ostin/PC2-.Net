using Microsoft.EntityFrameworkCore;
using S8_Yostin_Arequipa.DTOs;
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

    public async Task<IEnumerable<Order>> GetOrdersAfterDateAsync(DateTime date)
    {
        return await _dbSet
            .Where(o => o.OrderDate > date)
            .ToListAsync();
    }

    // 11: Obtener todos los productos vendidos a un cliente específico
    public async Task<IEnumerable<ProductSoldToClientDto>> GetProductsSoldToClientAsync(int clientId)
    {
        return await _dbSet
            .Where(o => o.ClientId == clientId)
            .SelectMany(o => o.Orderdetails)
            .Select(od => new ProductSoldToClientDto
            {
                ProductId = od.ProductId,
                ProductName = od.Product.Name,
                Price = od.Product.Price
            })
            .Distinct()
            .ToListAsync();
    }
}