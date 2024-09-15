using Auction.Core.Base.Common.Constants;
using Auction.Core.Logging.Common.Interfaces;
using Serilog.Core;
using Serilog.Events;

namespace Auction.Core.Logging.Service.Enrichers
{
    public sealed class RequestIdEnricher : ILogEventEnricher
    {
        private readonly ICallContext? _callContext; 
        public RequestIdEnricher(ICallContext callContext)
        {
            _callContext = callContext;
        }
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory) => logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(HttpHeaderConstants.XRequestId, _callContext?.GetContextId()));
    } 
}
