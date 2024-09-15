using Auction.Core.Base.Common.Constants;
using Auction.Core.Base.Common.Interfaces.HostConfigurer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Auction.Core.Middleware.Service.HostConfigurer
{
    internal class MiddlewareHostConfigurer : IHostConfigurer
    {
        public void Configure(WebApplication app)
        {  
            app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.ContainsKey(HttpHeaderConstants.XRequestId))
                {
                    context.Request.Headers.Add(HttpHeaderConstants.XRequestId, Guid.NewGuid().ToString());
                }

                await next.Invoke();
            });
        }
    }
}
