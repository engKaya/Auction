using Auction.Core.Repository.Common.Interface.UnitOfWork;
using Auction.User.Common.Infastructure.Repository;

namespace Auction.User.Common.Infastructure.UnitOfWork
{
    public interface IUserServiceUnitOfWork : IBaseUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }
}
