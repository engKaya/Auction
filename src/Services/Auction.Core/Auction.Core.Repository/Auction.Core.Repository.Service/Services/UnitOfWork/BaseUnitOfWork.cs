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
        private readonly ILogger<BaseUnitOfWork<T>> logger;
       // private readonly IMediator mediator;
        private IDbContextTransaction currentTransaction; 
        public bool HasActiveTransaction => currentTransaction != null;
        public BaseUnitOfWork(T context, ILogger<BaseUnitOfWork<T>> _logger/*, IMediator mediator*/)
        {
            this._dbContext = context;
         //   this.mediator = mediator;
            logger = _logger;
            currentTransaction = _dbContext.Database.BeginTransaction();
        }
        public IDbContextTransaction GetCurrentTransaction() => currentTransaction;

        public async Task<int> Commit(CancellationToken cancellation = default)
        {
            try
            {
                logger.LogInformation($"Saving Changes. Transaction Id: {this.currentTransaction.TransactionId}");

                //await mediator.DispatchDomainEventsAsync(_dbContext);

                int hasChanges = await _dbContext.SaveChangesAsync(cancellation);
                await currentTransaction.CommitAsync(cancellation);

                logger.LogInformation($"Transaction Id: {this.currentTransaction.TransactionId}, {hasChanges} changes committed!");

                return hasChanges; 
            }
            catch (Exception ex)
            {
                logger.LogCritical($"Error occured at commiting changes. Transaction Id: {this.currentTransaction.TransactionId}.\n Ex: {ex}\n Stack Trace: {ex.StackTrace} ");
                this.RollbackTransaction();
                throw;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                currentTransaction?.Rollback();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = default;
                }
            }
        }

        public async void Dispose()
        {
            logger.LogInformation($"Disposing UnitOfWork.{nameof(_dbContext)}");
            await _dbContext.DisposeAsync();
        }
    }
}
