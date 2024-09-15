namespace Auction.Core.Repository.Common.Interface.UnitOfWork
{
    public interface IBaseUnitOfWork : IDisposable
    {
        Task<int> Commit(CancellationToken cancellation = default);
        void RollbackTransaction(); 
        void ExecuteDbCommand(string spName);
    }
}
