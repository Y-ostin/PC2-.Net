using S8_Yostin_Arequipa.Data.Repositories;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Models;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(o => new OrderDto
        {
            OrderId = o.OrderId,
            ClientId = o.ClientId,
            OrderDate = o.OrderDate
        });
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return null;
        return new OrderDto
        {
            OrderId = order.OrderId,
            ClientId = order.ClientId,
            OrderDate = order.OrderDate
        };
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByClientIdAsync(int clientId)
    {
        var orders = await _orderRepository.GetOrdersByClientIdAsync(clientId);
        return orders.Select(o => new OrderDto
        {
            OrderId = o.OrderId,
            ClientId = o.ClientId,
            OrderDate = o.OrderDate
        });
    }

    public async Task AddOrderAsync(OrderDto orderDto)
    {
        var order = new Order
        {
            ClientId = orderDto.ClientId,
            OrderDate = orderDto.OrderDate
        };
        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task UpdateOrderAsync(OrderDto orderDto)
    {
        var order = await _orderRepository.GetByIdAsync(orderDto.OrderId);
        if (order != null)
        {
            order.ClientId = orderDto.ClientId;
            order.OrderDate = orderDto.OrderDate;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order != null)
        {
            await _orderRepository.DeleteAsync(order);
            await _orderRepository.SaveChangesAsync();
        }
    }
}