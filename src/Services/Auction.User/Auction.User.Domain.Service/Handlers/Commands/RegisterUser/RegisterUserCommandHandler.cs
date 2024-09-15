using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Middleware.Service.MediatR;
using Auction.User.Common.Infastructure.UnitOfWork;
using Auction.User.Entity.User;

namespace Auction.User.Domain.Service.Handlers.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : BaseRequestHandler<RegisterUserCommand>
    {
        private readonly IUserServiceUnitOfWork _uof; 
        public RegisterUserCommandHandler(ITraceService trace, IUserServiceUnitOfWork uof) : base(trace) 
        { 
            _uof = uof;
        }

        protected override async Task HandleRequest(RegisterUserCommand request, CancellationToken cancellationToken) 
        {    
            var user = new UserEntity
            {
                Email = request.Email,
                UserPhone = new List<UserPhoneEntity>
                {
                    new UserPhoneEntity
                    {
                        Phone = request.PhoneNumber,
                        Valid = true
                    }
                }
            };
             
            await _uof.UserRepository.AddAsync(user);
            await _uof.Commit(); 
        }
     }
}
