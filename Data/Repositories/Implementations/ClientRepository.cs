using Microsoft.EntityFrameworkCore;
using S8_Yostin_Arequipa.Models;

namespace S8_Yostin_Arequipa.Data.Repositories.Implementations;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Client?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Email == email);
    }

    public async Task<IEnumerable<Client>> GetClientsByNameStartingWithAsync(string prefix)
    {
        return await _dbSet.Where(c => c.Name.StartsWith(prefix)).ToListAsync();
    }

    // 9: Obtener el cliente con mayor número de pedidos
    public async Task<(int ClientId, string ClientName, int OrderCount)?> GetClientWithMostOrdersAsync()
    {
        var result = await _dbSet
            .Include(c => c.Orders)
            .GroupBy(c => new { c.ClientId, c.Name })
            .Select(g => new
            {
                ClientId = g.Key.ClientId,
                ClientName = g.Key.Name,
                OrderCount = g.Sum(c => c.Orders.Count)
            })
            .OrderByDescending(x => x.OrderCount)
            .FirstOrDefaultAsync();

        if (result == null)
            return null;

        return (result.ClientId, result.ClientName, result.OrderCount);
    }
}