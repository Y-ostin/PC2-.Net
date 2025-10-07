using S8_Yostin_Arequipa.Data.Repositories;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Models;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Services;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _orderDetailRepository;

    public OrderDetailService(IOrderDetailRepository orderDetailRepository)
    {
        _orderDetailRepository = orderDetailRepository;
    }

    public async Task<IEnumerable<OrderDetailDto>> GetAllOrderDetailsAsync()
    {
        var orderDetails = await _orderDetailRepository.GetAllAsync();
        return orderDetails.Select(od => new OrderDetailDto
        {
            OrderDetailId = od.OrderDetailId,
            OrderId = od.OrderId,
            ProductId = od.ProductId,
            Quantity = od.Quantity
        });
    }

    public async Task<OrderDetailDto?> GetOrderDetailByIdAsync(int id)
    {
        var orderDetail = await _orderDetailRepository.GetByIdAsync(id);
        if (orderDetail == null) return null;
        return new OrderDetailDto
        {
            OrderDetailId = orderDetail.OrderDetailId,
            OrderId = orderDetail.OrderId,
            ProductId = orderDetail.ProductId,
            Quantity = orderDetail.Quantity
        };
    }

    public async Task<IEnumerable<OrderDetailDto>> GetOrderDetailsByOrderIdAsync(int orderId)
    {
        var orderDetails = await _orderDetailRepository.GetDetailsByOrderIdAsync(orderId);
        return orderDetails.Select(od => new OrderDetailDto
        {
            OrderDetailId = od.OrderDetailId,
            OrderId = od.OrderId,
            ProductId = od.ProductId,
            Quantity = od.Quantity
        });
    }

    public async Task AddOrderDetailAsync(OrderDetailDto orderDetailDto)
    {
        var orderDetail = new Orderdetail
        {
            OrderId = orderDetailDto.OrderId,
            ProductId = orderDetailDto.ProductId,
            Quantity = orderDetailDto.Quantity
        };
        await _orderDetailRepository.AddAsync(orderDetail);
        await _orderDetailRepository.SaveChangesAsync();
    }

    public async Task UpdateOrderDetailAsync(OrderDetailDto orderDetailDto)
    {
        var orderDetail = await _orderDetailRepository.GetByIdAsync(orderDetailDto.OrderDetailId);
        if (orderDetail != null)
        {
            orderDetail.OrderId = orderDetailDto.OrderId;
            orderDetail.ProductId = orderDetailDto.ProductId;
            orderDetail.Quantity = orderDetailDto.Quantity;
            await _orderDetailRepository.UpdateAsync(orderDetail);
            await _orderDetailRepository.SaveChangesAsync();
        }
    }

    public async Task DeleteOrderDetailAsync(int id)
    {
        var orderDetail = await _orderDetailRepository.GetByIdAsync(id);
        if (orderDetail != null)
        {
            await _orderDetailRepository.DeleteAsync(orderDetail);
            await _orderDetailRepository.SaveChangesAsync();
        }
    }
}