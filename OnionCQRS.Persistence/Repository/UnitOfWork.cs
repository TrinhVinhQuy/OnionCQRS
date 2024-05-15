using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using OnionCQRS.Domain.Abstracts;
using OnionCQRS.Persistence.Data;

namespace OnionCQRS.Persistence.Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly ISQLQueryHandler _sqLQueryHandler;

        ApplicationDbContext _applicationDbContext;
        
        //IUserRepository _userRepository;

        IDbContextTransaction _dbContextTransaction;
        private bool disposedValue;

        public UnitOfWork(ApplicationDbContext applicationDbContext, 
            ISQLQueryHandler sQLQueryHandler)
        {
            _applicationDbContext = applicationDbContext;
            _sqLQueryHandler = sQLQueryHandler;
        }

        public DbSet<T> Table<T>() where T : class => _applicationDbContext.Set<T>();

        //public IUserRepository userRepository => _userRepository ??= new UserRepository(_applicationDbContext);

        public async Task BeginTransaction()
        {
            _dbContextTransaction = await _applicationDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContextTransaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _dbContextTransaction.RollbackAsync();
        }

        public async Task SaveChangeAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
