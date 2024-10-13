using Auction.Core.Base.Common.Infastructure;
using MediatR;

namespace Auction.Core.Middleware.Common.MediatR.Base
{
    public interface IBaseRequestHandler<TRequest,TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : BaseRequest,IRequest<TResponse>
        where TResponse : BaseResponse 
    {

    }

    public interface IBaseRequestHandler<TRequest> : IRequestHandler<TRequest>
        where TRequest : BaseRequest, IRequest
    {

    }
}
