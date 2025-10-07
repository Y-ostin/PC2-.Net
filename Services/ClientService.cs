using S8_Yostin_Arequipa.Data.Repositories;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Models;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
    {
        var clients = await _clientRepository.GetAllAsync();
        return clients.Select(c => new ClientDto
        {
            ClientId = c.ClientId,
            Name = c.Name,
            Email = c.Email
        });
    }

    public async Task<ClientDto?> GetClientByIdAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
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
        var client = await _clientRepository.GetByEmailAsync(email);
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
        await _clientRepository.AddAsync(client);
        await _clientRepository.SaveChangesAsync();
    }

    public async Task UpdateClientAsync(ClientDto clientDto)
    {
        var client = await _clientRepository.GetByIdAsync(clientDto.ClientId);
        if (client != null)
        {
            client.Name = clientDto.Name;
            client.Email = clientDto.Email;
            await _clientRepository.UpdateAsync(client);
            await _clientRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteClientAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client != null)
        {
            await _clientRepository.DeleteAsync(client);
            await _clientRepository.SaveChangesAsync();
        }
    }
}