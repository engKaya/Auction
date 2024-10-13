using Auction.Core.Base.Common.Infastructure;
using MediatR;

namespace Auction.User.Domain.Service.Handlers.Queries.User
{
    public class LoginQuery : BaseRequest, IRequest<BaseResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
