using Auction.Core.Logging.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Auction.Core.Middleware.Common
{
    public abstract class BaseMiddleware
    {
        private readonly RequestDelegate _next;
        public ICallContext _callContext { get; private set; }
        public BaseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICallContext callContext)
        { 
            _callContext = callContext;
            await Execute(context);
            await _next(context);
        }

        public abstract Task Execute(HttpContext context); 
    }
}
