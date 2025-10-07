using S8_Yostin_Arequipa.Data.Repositories;
using S8_Yostin_Arequipa.Data.Repositories.Implementations;

namespace S8_Yostin_Arequipa.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IClientRepository? _clients;
    private IProductRepository? _products;
    private IOrderRepository? _orders;
    private IOrderDetailRepository? _orderDetails;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IClientRepository Clients => _clients ??= new ClientRepository(_context);
    public IProductRepository Products => _products ??= new ProductRepository(_context);
    public IOrderRepository Orders => _orders ??= new OrderRepository(_context);
    public IOrderDetailRepository OrderDetails => _orderDetails ??= new OrderDetailRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}