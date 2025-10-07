using S8_Yostin_Arequipa.Data.UnitOfWork;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Models;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _unitOfWork.Orders.GetAllAsync();
        return orders.Select(o => new OrderDto
        {
            OrderId = o.OrderId,
            ClientId = o.ClientId,
            OrderDate = o.OrderDate
        });
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id);
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
        var orders = await _unitOfWork.Orders.GetOrdersByClientIdAsync(clientId);
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
        await _unitOfWork.Orders.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateOrderAsync(OrderDto orderDto)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderDto.OrderId);
        if (order != null)
        {
            order.ClientId = orderDto.ClientId;
            order.OrderDate = orderDto.OrderDate;
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(id);
        if (order != null)
        {
            await _unitOfWork.Orders.DeleteAsync(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}