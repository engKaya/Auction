using Auction.Core.Logging.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Auction.Core.Middleware.Common
{
    public abstract class BaseMiddleware
    {
        private readonly RequestDelegate _next; 

        protected ICallContext _callContext { get; private set; }
        protected ITraceService _trace { get; private set; }
        public BaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICallContext callContext, ITraceService trace)
        { 
            _callContext = callContext;  
            _trace = trace;
            await Execute(context);
            await _next(context);
        }

        public abstract Task Execute(HttpContext context); 
    }
}
