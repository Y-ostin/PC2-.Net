using S8_Yostin_Arequipa.Data.UnitOfWork;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Models;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Services;

public class ClientService : IClientService
{
    private readonly IUnitOfWork _unitOfWork;

    public ClientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
    {
        var clients = await _unitOfWork.Clients.GetAllAsync();
        return clients.Select(c => new ClientDto
        {
            ClientId = c.ClientId,
            Name = c.Name,
            Email = c.Email
        });
    }

    public async Task<ClientDto?> GetClientByIdAsync(int id)
    {
        var client = await _unitOfWork.Clients.GetByIdAsync(id);
        if (client == null) return null;
        return new ClientDto
        {
            ClientId = client.ClientId,
            Name = client.Name,
            Email = client.Email
        };
    }

    public async Task<ClientDto?> GetClientByEmailAsync(string email)
    {
        var client = await _unitOfWork.Clients.GetByEmailAsync(email);
        if (client == null) return null;
        return new ClientDto
        {
            ClientId = client.ClientId,
            Name = client.Name,
            Email = client.Email
        };
    }

    public async Task AddClientAsync(ClientDto clientDto)
    {
        var client = new Client
        {
            Name = clientDto.Name,
            Email = clientDto.Email
        };
        await _unitOfWork.Clients.AddAsync(client);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateClientAsync(ClientDto clientDto)
    {
        var client = await _unitOfWork.Clients.GetByIdAsync(clientDto.ClientId);
        if (client != null)
        {
            client.Name = clientDto.Name;
            client.Email = clientDto.Email;
            await _unitOfWork.Clients.UpdateAsync(client);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task DeleteClientAsync(int id)
    {
        var client = await _unitOfWork.Clients.GetByIdAsync(id);
        if (client != null)
        {
            await _unitOfWork.Clients.DeleteAsync(client);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ClientDto>> GetClientsByNameStartingWithAsync(string prefix)
    {
        var clients = await _unitOfWork.Clients.GetClientsByNameStartingWithAsync(prefix);
        return clients.Select(c => new ClientDto
        {
            ClientId = c.ClientId,
            Name = c.Name,
            Email = c.Email
        });
    }

    // 9: Obtener el cliente con mayor número de pedidos
    public async Task<ClientOrderCountDto?> GetClientWithMostOrdersAsync()
    {
        var result = await _unitOfWork.Clients.GetClientWithMostOrdersAsync();
        
        if (result == null)
            return null;

        return new ClientOrderCountDto
        {
            ClientId = result.Value.ClientId,
            ClientName = result.Value.ClientName,
            OrderCount = result.Value.OrderCount
        };
    }
}