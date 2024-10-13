using Auction.Core.Repository.Common.Interface.UnitOfWork;

namespace Auction.Core.Repository.Common.Interface
{
    public interface ITransactionRepository
    {
        public Uof CreateTransaction<Uof>() where Uof : IBaseUnitOfWork;
        public void CommitTransaction();
        public void RollbackTransaction();
    }
}
