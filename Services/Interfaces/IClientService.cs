using S8_Yostin_Arequipa.DTOs;

namespace S8_Yostin_Arequipa.Services.Interfaces;

public interface IClientService
{
    Task<IEnumerable<ClientDto>> GetAllClientsAsync();
    Task<ClientDto?> GetClientByIdAsync(int id);
    Task<ClientDto?> GetClientByEmailAsync(string email);
    Task<IEnumerable<ClientDto>> GetClientsByNameStartingWithAsync(string prefix);
    Task AddClientAsync(ClientDto clientDto);
    Task UpdateClientAsync(ClientDto clientDto);
    Task DeleteClientAsync(int id);
    Task<ClientOrderCountDto?> GetClientWithMostOrdersAsync();
}