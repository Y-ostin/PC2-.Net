using Microsoft.AspNetCore.Mvc;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailsController : ControllerBase
{
    private readonly IOrderDetailService _orderDetailService;

    public OrderDetailsController(IOrderDetailService orderDetailService)
    {
        _orderDetailService = orderDetailService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orderDetails = await _orderDetailService.GetAllOrderDetailsAsync();
        return Ok(orderDetails);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);
        if (orderDetail == null) return NotFound();
        return Ok(orderDetail);
    }

    [HttpGet("order/{orderId}")]
    public async Task<IActionResult> GetByOrderId(int orderId)
    {
        var orderDetails = await _orderDetailService.GetOrderDetailsByOrderIdAsync(orderId);
        return Ok(orderDetails);
    }

    [HttpGet("order/{orderId}/products")]
    public async Task<IActionResult> GetProductDetailsByOrderId(int orderId)
    {
        var productDetails = await _orderDetailService.GetProductDetailsByOrderIdAsync(orderId);
        return Ok(productDetails);
    }

    [HttpGet("order/{orderId}/total-quantity")]
    public async Task<IActionResult> GetTotalQuantityByOrderId(int orderId)
    {
        var totalQuantity = await _orderDetailService.GetTotalQuantityByOrderIdAsync(orderId);
        return Ok(totalQuantity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderDetailDto orderDetailDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _orderDetailService.AddOrderDetailAsync(orderDetailDto);
        return CreatedAtAction(nameof(GetById), new { id = orderDetailDto.OrderDetailId }, orderDetailDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderDetailDto orderDetailDto)
    {
        if (id != orderDetailDto.OrderDetailId) return BadRequest();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _orderDetailService.UpdateOrderDetailAsync(orderDetailDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _orderDetailService.DeleteOrderDetailAsync(id);
        return NoContent();
    }

    // 10: Obtener todos los pedidos con sus detalles (nombre del producto y cantidad)
    [HttpGet("all-with-details")]
    public async Task<ActionResult<IEnumerable<OrderWithDetailsDto>>> GetAllOrdersWithDetails()
    {
        var result = await _orderDetailService.GetAllOrdersWithDetailsAsync();
        return Ok(result);
    }

    // 12: Obtener todos los clientes que han comprado un producto específico
    [HttpGet("product/{productId}/clients")]
    public async Task<ActionResult<IEnumerable<ClientWhoBoughtProductDto>>> GetClientsWhoBoughtProduct(int productId)
    {
        var clients = await _orderDetailService.GetClientsWhoBoughtProductAsync(productId);
        return Ok(clients);
    }
}