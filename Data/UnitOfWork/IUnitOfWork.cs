using S8_Yostin_Arequipa.Data.Repositories;

namespace S8_Yostin_Arequipa.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IClientRepository Clients { get; }
    IProductRepository Products { get; }
    IOrderRepository Orders { get; }
    IOrderDetailRepository OrderDetails { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}