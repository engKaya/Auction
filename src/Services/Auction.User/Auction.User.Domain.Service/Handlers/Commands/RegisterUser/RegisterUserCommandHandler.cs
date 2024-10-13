using Auction.Core.Base.Common.Infastructure;
using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Middleware.Service.MediatR;
using Auction.Core.Repository.Common.Interface;
using Auction.User.Common.Infastructure.UnitOfWork;
using Auction.User.Entity.User;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.User.Domain.Service.Handlers.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : BaseRequestHandler<RegisterUserCommand, BaseResponse>
    {
        private readonly IUserServiceUnitOfWork _uof;
        private readonly IServiceScopeFactory _provider;
        private readonly ICallContext _callContext;
        public RegisterUserCommandHandler(ITraceService trace, IServiceScopeFactory provider, ICallContext callContext) : base(trace)
        {
            _provider = provider;
            _callContext = callContext;
        }

        protected override async Task<BaseResponse> HandleRequest(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();


            using (var scope = _provider.CreateScope())
            { 
                var innerCallContext = scope.ServiceProvider.GetRequiredService<ICallContext>().NewContextId();
                var transcationRepo = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
                var uof = transcationRepo.CreateTransaction<IUserServiceUnitOfWork>();
                try
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
                    await uof.UserRepository.AddAsync(user);
                    transcationRepo.CommitTransaction();
                    return response;
                }
                catch (Exception ex)
                {
                    transcationRepo.RollbackTransaction();
                    response.SetException(ex);
                    return response;
                }
            }

        }
    }
}
