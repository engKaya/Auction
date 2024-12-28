using Auction.Core.Base.Common.Infastructure;
using MediatR;

namespace Auction.User.Domain.Service.Handlers.Queries.User
{
    public class LoginQuery : BaseRequest, IRequest<BaseResponse<LoginQueryResponse>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginQueryResponse 
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Expire { get; set; } = DateTime.MinValue;
    }
}
