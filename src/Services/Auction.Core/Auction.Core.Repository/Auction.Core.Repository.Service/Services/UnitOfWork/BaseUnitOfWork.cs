using Auction.Core.Middleware.Service.DomainEvents;
using Auction.Core.Repository.Common.Context;
using Auction.Core.Repository.Common.Interface.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;


namespace Auction.Core.Repository.Service.Services.UnitOfWork
{
    public class BaseUnitOfWork<T> : IBaseUnitOfWork where T : BaseDbContext
    {
        protected readonly T _dbContext;
        private readonly ILogger<BaseUnitOfWork<T>> _logger;
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction; 
        public bool HasActiveTransaction => _currentTransaction != null;
        public BaseUnitOfWork(T context, ILogger<BaseUnitOfWork<T>> logger, IMediator mediator)
        {
            _dbContext = context; 
            _logger = logger;
            _currentTransaction = _dbContext.Database.BeginTransaction(); 
            _mediator = mediator;
        }
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public async Task<int> Commit(CancellationToken cancellation = default)
        {
            try
            {
                _logger.LogInformation($"Saving Changes. Transaction Id: {_currentTransaction.TransactionId}");

                await _mediator.DispatchDomainEventsAsync(_dbContext);

                int hasChanges = await _dbContext.SaveChangesAsync(cancellation);
                await _currentTransaction.CommitAsync(cancellation);

                _logger.LogInformation($"Transaction Id: {_currentTransaction.TransactionId}, {hasChanges} changes committed!");

                return hasChanges; 
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Error occured at commiting changes. Transaction Id: {_currentTransaction.TransactionId}.\n Ex: {ex}\n Stack Trace: {ex.StackTrace} ");
                this.RollbackTransaction();
                throw;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = default;
                }
            }
        }

        public async void Dispose()
        {
            _logger.LogInformation($"Disposing UnitOfWork.{_dbContext.GetType().FullName}"); 
            await _dbContext.DisposeAsync();
        }
    }
}
