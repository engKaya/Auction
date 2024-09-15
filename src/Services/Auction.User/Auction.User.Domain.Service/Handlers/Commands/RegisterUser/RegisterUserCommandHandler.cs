using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Middleware.Service.MediatR;

namespace Auction.User.Domain.Service.Handlers.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : BaseRequestHandler<RegisterUserCommand>
    {
        public RegisterUserCommandHandler(ITraceService trace) : base(trace) 
        { 
            
        }

        protected override Task HandleRequest(RegisterUserCommand request, CancellationToken cancellationToken) 
        { 
            Console.WriteLine("RegisterUserCommandHandler");

            return Task.CompletedTask;
        }
     }
}
