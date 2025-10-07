using Microsoft.AspNetCore.Mvc;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> GetByClientId(int clientId)
    {
        var orders = await _orderService.GetOrdersByClientIdAsync(clientId);
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderDto orderDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _orderService.AddOrderAsync(orderDto);
        return CreatedAtAction(nameof(GetById), new { id = orderDto.OrderId }, orderDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderDto orderDto)
    {
        if (id != orderDto.OrderId) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _orderService.UpdateOrderAsync(orderDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderService.DeleteOrderAsync(id);
        return NoContent();
    }
}