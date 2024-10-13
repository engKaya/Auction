using Auction.Core.Base.Common.Constants;
using Auction.Core.Logging.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Serilog.Events;

namespace Auction.Core.Logging.Service.Enrichers
{
    public sealed class RequestIdEnricher : ILogEventEnricher
    {
        private readonly IServiceProvider _serviceProvider; 
        public RequestIdEnricher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var callContext = _serviceProvider.GetRequiredService<ICallContext>();
            var httpContext = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(HttpHeaderConstants.XRequestId, httpContext.HttpContext?.Request.Headers[HttpHeaderConstants.XRequestId].ToString() ?? callContext.ContextId));
        }
    }
}
