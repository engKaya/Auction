using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Repository.Common.Interface;
using Auction.Core.Repository.Common.Interface.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace Auction.Core.Repository.Service.Services
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICallContext _callContext;
        public TransactionRepository(IServiceProvider serviceProvider, ICallContext callContext)
        {
            _serviceProvider = serviceProvider;
            _callContext = callContext;
        }
        private ConcurrentDictionary<string, List<IBaseUnitOfWork>> _transactionList = new ConcurrentDictionary<string, List<IBaseUnitOfWork>>();

        public Uof CreateTransaction<Uof>() where Uof : IBaseUnitOfWork
        {
            var transaction = _serviceProvider.GetRequiredService<Uof>();
            if (!_transactionList.ContainsKey(_callContext.ContextId))
            {
                _transactionList.TryAdd(_callContext.ContextId, new List<IBaseUnitOfWork>() { transaction });
            }
            else
            {
                _transactionList[_callContext.ContextId].Add(transaction);
            }
            return transaction;
        }

        public void CommitTransaction()
        {
            if (_transactionList.ContainsKey(_callContext.ContextId))
            {
                foreach (var transaction in _transactionList[_callContext.ContextId])
                {
                    transaction.Commit();
                }
                _transactionList.TryRemove(_callContext.ContextId, out _);
            }
        }

        public void RollbackTransaction()
        {
            if (_transactionList.ContainsKey(_callContext.ContextId))
            {
                foreach (var transaction in _transactionList[_callContext.ContextId])
                {
                    transaction.RollbackTransaction();
                }
                _transactionList.TryRemove(_callContext.ContextId, out _);
            }
        }
    }
}
