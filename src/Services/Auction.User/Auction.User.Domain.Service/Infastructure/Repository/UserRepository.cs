using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Repository.Service.Services.Repository;
using Auction.User.Base.DbContext;
using Auction.User.Common.Infastructure.Repository;
using Auction.User.Entity.User;

namespace Auction.User.Domain.Service.Infastructure.Repository
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(UserDbContext dbContext, ICallContext callContext) : base(dbContext, callContext)
        {
        }
    }
}
