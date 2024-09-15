using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Repository.Service.Services.UnitOfWork;
using Auction.User.Base.DbContext;
using Auction.User.Common.Infastructure.Repository;
using Auction.User.Common.Infastructure.UnitOfWork;
using Auction.User.Domain.Service.Infastructure.Repository;
using MediatR;

namespace Auction.User.Domain.Service.Infastructure.UnitOfWork
{
    public class UserServiceUnitOfWork : BaseUnitOfWork<UserDbContext>, IUserServiceUnitOfWork
    { 
        public UserServiceUnitOfWork(UserDbContext context, ITraceService trace, IMediator mediator, ICallContext callContext) : base(context, trace, mediator, callContext)
        { 
        }

        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                userRepository = new UserRepository(this._dbContext, _callContext);
                return userRepository;
            }
        }

        private IUserPhoneRepository userPhoneRepository;
        public IUserPhoneRepository UserPhoneRepository
        {
            get
            {
                userPhoneRepository = new UserPhoneRepository(this._dbContext, _callContext);
                return userPhoneRepository;
            }
        }
    }
}
