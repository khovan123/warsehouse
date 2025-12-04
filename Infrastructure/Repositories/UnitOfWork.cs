using Application;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<Type, object> _repositories;
    private readonly Dictionary<Type, Func<ApplicationDbContext, object>> _repositoryFactories;
    private IDbContextTransaction? _transaction;

    #region System Repositories

    //public IUsersRepository Users => (IUsersRepository)Repository<User>();

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
        _repositoryFactories = new Dictionary<Type, Func<ApplicationDbContext, object>>
        {
            //{ typeof(User), ctx => new UsersRepository(ctx) },
        };
    }

    public IGenericRepository<T> Repository<T>() where T : EntityBase<Guid>
    {
        var type = typeof(T);

        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = _repositoryFactories.TryGetValue(type, out var factory)
                ? factory(_context)
                : new GenericRepository<T>(_context);
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    #endregion

    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        => await _context.SaveChangesAsync(ct);

    public async Task BeginTransactionAsync(CancellationToken ct = default)
        => _transaction = await _context.Database.BeginTransactionAsync(ct);

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken ct = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}