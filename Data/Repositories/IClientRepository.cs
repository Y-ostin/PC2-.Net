using S8_Yostin_Arequipa.Models;

namespace S8_Yostin_Arequipa.Data.Repositories;

public interface IClientRepository : IRepository<Client>
{
    // Métodos específicos para Client si es necesario
    Task<Client?> GetByEmailAsync(string email);
    Task<IEnumerable<Client>> GetClientsByNameStartingWithAsync(string prefix);
    Task<(int ClientId, string ClientName, int OrderCount)?> GetClientWithMostOrdersAsync();
}