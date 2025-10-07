using Microsoft.AspNetCore.Mvc;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clientService.GetAllClientsAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);
        if (client == null) return NotFound();
        return Ok(client);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var client = await _clientService.GetClientByEmailAsync(email);
        if (client == null) return NotFound();
        return Ok(client);
    }

    [HttpGet("name/{prefix}")]
    public async Task<IActionResult> GetByNameStartingWith(string prefix)
    {
        var clients = await _clientService.GetClientsByNameStartingWithAsync(prefix);
        return Ok(clients);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientDto clientDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _clientService.AddClientAsync(clientDto);
        return CreatedAtAction(nameof(GetById), new { id = clientDto.ClientId }, clientDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ClientDto clientDto)
    {
        if (id != clientDto.ClientId) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _clientService.UpdateClientAsync(clientDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _clientService.DeleteClientAsync(id);
        return NoContent();
    }
}