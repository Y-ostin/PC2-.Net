using Microsoft.EntityFrameworkCore;
using S8_Yostin_Arequipa.DTOs;
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

    // 3: Obtener detalles de productos en una orden
    public async Task<IEnumerable<ProductDetailInOrderDto>> GetProductDetailsByOrderIdAsync(int orderId)
    {
        var result = await _dbSet
            .Where(od => od.OrderId == orderId)
            .Include(od => od.Product)
            .Select(od => new ProductDetailInOrderDto
            {
                ProductName = od.Product.Name,
                Quantity = od.Quantity
            })
            .ToListAsync();
        
        return result;
    }

    // 4: Obtener cantidad total de productos por orden
    public async Task<int> GetTotalQuantityByOrderIdAsync(int orderId)
    {
        return await _dbSet
            .Where(od => od.OrderId == orderId)
            .Select(od => od.Quantity)
            .SumAsync();
    }

    // 10: Obtener todos los pedidos con sus detalles (nombre del producto y cantidad)
    public async Task<IEnumerable<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync()
    {
        return await _dbSet
            .Include(od => od.Order)
            .Include(od => od.Product)
            .Select(od => new OrderWithDetailsDto
            {
                OrderId = od.OrderId,
                OrderDate = od.Order.OrderDate,
                ProductName = od.Product.Name,
                Quantity = od.Quantity
            })
            .ToListAsync();
    }

    // 12: Obtener todos los clientes que han comprado un producto específico
    public async Task<IEnumerable<ClientWhoBoughtProductDto>> GetClientsWhoBoughtProductAsync(int productId)
    {
        return await _dbSet
            .Where(od => od.ProductId == productId)
            .Include(od => od.Order)
            .ThenInclude(o => o.Client)
            .Select(od => new ClientWhoBoughtProductDto
            {
                ClientId = od.Order.Client.ClientId,
                ClientName = od.Order.Client.Name,
                Email = od.Order.Client.Email
            })
            .Distinct()
            .ToListAsync();
    }
}