using S8_Yostin_Arequipa.Data.UnitOfWork;
using S8_Yostin_Arequipa.DTOs;
using S8_Yostin_Arequipa.Models;
using S8_Yostin_Arequipa.Services.Interfaces;

namespace S8_Yostin_Arequipa.Services;

public class OrderDetailService : IOrderDetailService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderDetailService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<OrderDetailDto>> GetAllOrderDetailsAsync()
    {
        var orderDetails = await _unitOfWork.OrderDetails.GetAllAsync();
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
        var orderDetail = await _unitOfWork.OrderDetails.GetByIdAsync(id);
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
        var orderDetails = await _unitOfWork.OrderDetails.GetDetailsByOrderIdAsync(orderId);
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
        await _unitOfWork.OrderDetails.AddAsync(orderDetail);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateOrderDetailAsync(OrderDetailDto orderDetailDto)
    {
        var orderDetail = await _unitOfWork.OrderDetails.GetByIdAsync(orderDetailDto.OrderDetailId);
        if (orderDetail != null)
        {
            orderDetail.OrderId = orderDetailDto.OrderId;
            orderDetail.ProductId = orderDetailDto.ProductId;
            orderDetail.Quantity = orderDetailDto.Quantity;
            await _unitOfWork.OrderDetails.UpdateAsync(orderDetail);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    // 3: Obtener detalles de productos en una orden
    public async Task<IEnumerable<ProductDetailInOrderDto>> GetProductDetailsByOrderIdAsync(int orderId)
    {
        var productDetails = await _unitOfWork.OrderDetails.GetProductDetailsByOrderIdAsync(orderId);
        return productDetails.Select(pd => new ProductDetailInOrderDto
        {
            ProductName = ((dynamic)pd).ProductName,
            Quantity = ((dynamic)pd).Quantity
        });
    }

    // 4: Obtener cantidad total de productos por orden
    public async Task<OrderTotalQuantityDto> GetTotalQuantityByOrderIdAsync(int orderId)
    {
        var totalQuantity = await _unitOfWork.OrderDetails.GetTotalQuantityByOrderIdAsync(orderId);
        return new OrderTotalQuantityDto
        {
            OrderId = orderId,
            TotalQuantity = totalQuantity
        };
    }

    public async Task DeleteOrderDetailAsync(int id)
    {
        var orderDetail = await _unitOfWork.OrderDetails.GetByIdAsync(id);
        if (orderDetail != null)
        {
            await _unitOfWork.OrderDetails.DeleteAsync(orderDetail);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    // 10: Obtener todos los pedidos con sus detalles
    public async Task<IEnumerable<OrderWithDetailsDto>> GetAllOrdersWithDetailsAsync()
    {
        return await _unitOfWork.OrderDetails.GetAllOrdersWithDetailsAsync();
    }

    // 12: Obtener todos los clientes que han comprado un producto específico
    public async Task<IEnumerable<ClientWhoBoughtProductDto>> GetClientsWhoBoughtProductAsync(int productId)
    {
        return await _unitOfWork.OrderDetails.GetClientsWhoBoughtProductAsync(productId);
    }
}