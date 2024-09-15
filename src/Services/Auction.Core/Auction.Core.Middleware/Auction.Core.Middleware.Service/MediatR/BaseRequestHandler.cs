using Auction.Core.Base.Common.Infastructure;
using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Middleware.Common.MediatR.Base;
using Azure;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Auction.Core.Middleware.Service.MediatR
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IBaseRequestHandler<TRequest, TResponse> where TRequest : BaseRequest, IRequest<TResponse> where TResponse : BaseResponse<TResponse>
    {
        private readonly ITraceService _trace;

        protected BaseRequestHandler(ITraceService trace)
        {
            _trace = trace;               
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        { 
            _trace.Log($"Handling {typeof(TRequest).Name}, Request Id: {request.RequestId}, Request Type: {nameof(request.RequestTypes)}", request );

            var response = await HandleRequest(request, cancellationToken);

          _trace.Log($"Handled {typeof(TRequest).Name}, Request Id: {request.RequestId}", response);

            return response;
        } 
        protected abstract Task<TResponse> HandleRequest(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class BaseRequestHandler<TRequest> : IBaseRequestHandler<TRequest> where TRequest : BaseRequest, IRequest
    {
        private readonly ITraceService _trace;

        protected BaseRequestHandler(ITraceService trace)
        {
            _trace = trace;               
        }

        public async Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            _trace.Log($"Handling {typeof(TRequest).Name}, Request Id: {request.RequestId}, Request Type: {request.RequestTypes}", request);

            await HandleRequest(request, cancellationToken);

            _trace.Log($"Handled {typeof(TRequest).Name}, Request Id: {request.RequestId}");
        } 
        protected abstract Task HandleRequest(TRequest request, CancellationToken cancellationToken);
    }
}
