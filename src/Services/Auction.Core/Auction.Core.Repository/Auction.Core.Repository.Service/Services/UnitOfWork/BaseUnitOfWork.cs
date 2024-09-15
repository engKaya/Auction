using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Middleware.Service.DomainEvents;
using Auction.Core.Repository.Common.Context;
using Auction.Core.Repository.Common.Interface.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;


namespace Auction.Core.Repository.Service.Services.UnitOfWork
{
    public class BaseUnitOfWork<T> : IBaseUnitOfWork where T : BaseDbContext
    {
        protected readonly T _dbContext;
        private readonly ITraceService _trace;
        private readonly IMediator _mediator;
        protected readonly ICallContext _callContext;
        private IDbContextTransaction _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public BaseUnitOfWork(T context, ITraceService trace, IMediator mediator, ICallContext callContext)
        {
            _dbContext = context;
            _trace = trace;
            _currentTransaction = _dbContext.Database.BeginTransaction();
            _mediator = mediator;
            _callContext = callContext;
        }
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public async Task<int> Commit(CancellationToken cancellation = default)
        {
            var timer = Stopwatch.StartNew();
            try
            {

                _trace.Log($"Saving Changes. Transaction Id: {_currentTransaction.TransactionId}, CallContext Id: {_callContext.ContextId}");

                await _mediator.DispatchDomainEventsAsync(_dbContext);

                int hasChanges = await _dbContext.SaveChangesAsync(cancellation);
                await _currentTransaction.CommitAsync(cancellation);

                timer.Stop();
                _trace.Log($"Transaction Id: {_currentTransaction.TransactionId}, {hasChanges} changes committed!, CallContext Id: {_callContext.ContextId}, Total elapsed: {timer.ElapsedMilliseconds} ms");

                return hasChanges;
            }
            catch (Exception ex)
            {
                if (timer.IsRunning)
                    timer.Stop();

                _trace.Log($"Error occured at commiting changes, CallContext Id: {_callContext.ContextId}. Transaction Id: {_currentTransaction.TransactionId}.\n Ex: {ex}\n Stack Trace: {ex.StackTrace}, total elapsed: {timer.ElapsedMilliseconds} ms");
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
            _trace.Log($"Disposing UnitOfWork.{_dbContext.GetType().FullName}, CallContext Id: {_callContext.ContextId}");
            await _dbContext.DisposeAsync();
        }

        public void ExecuteDbCommand(string spName)
        {
            _trace.Log($"Executing Db Command: {spName}, CallContext Id: {_callContext.ContextId}");
            _dbContext.Database.ExecuteSqlRaw(spName);
        }
    }
}
