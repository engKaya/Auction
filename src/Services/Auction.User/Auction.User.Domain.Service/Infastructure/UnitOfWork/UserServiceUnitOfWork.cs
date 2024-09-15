using Auction.Core.Repository.Service.Services.UnitOfWork;
using Auction.User.Base.DbContext;
using Auction.User.Common.Infastructure.Repository;
using Auction.User.Common.Infastructure.UnitOfWork;
using Auction.User.Domain.Service.Infastructure.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Auction.User.Domain.Service.Infastructure.UnitOfWork
{
    public class UserServiceUnitOfWork : BaseUnitOfWork<UserDbContext>, IUserServiceUnitOfWork
    {
        public UserServiceUnitOfWork(UserDbContext context, ILogger<BaseUnitOfWork<UserDbContext>> _logger, IMediator mediator) : base(context, _logger, mediator)
        {
        }

        private IUserRepository userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                userRepository = new UserRepository(this._dbContext);
                return userRepository;
            }
        }

        private IUserPhoneRepository userPhoneRepository;
        public IUserPhoneRepository UserPhoneRepository
        {
            get
            {
                userPhoneRepository = new UserPhoneRepository(this._dbContext);
                return userPhoneRepository;
            }
        }
    }
}
