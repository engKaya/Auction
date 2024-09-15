using Auction.Core.Base.Common;
using Auction.Core.Logging.Common.Classes;
using Auction.Core.Logging.Common.Constants;
using Auction.Core.Logging.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Auction.Core.Logging.Service.Infastructure
{
    public sealed class TraceService : ITraceService
    {
        ILogger<LogItem> _logger; 
        private readonly string RequestId;
        private Action<Exception?, string?, object[]> action;


        public TraceService(ILogger<LogItem> logger, ICallContext _callContext)
        {
            _logger = logger;
            RequestId = _callContext.ContextId;
            setLogAction(Environment.GetEnvironmentVariable(EnviromentConstants.LOG_LEVEL));
        }
        public void Log(LogItem logItem)
        {  
            var stringProperties = string.Empty;
            if (logItem.Properties != null)
            {
                foreach (var prop in logItem.Properties)
                { 
                    stringProperties += $"{prop.Key}: {prop.Value}|";
                }
            }

            if (!string.IsNullOrEmpty(stringProperties)) 
                logItem.Message += $"|Properties: {stringProperties}";

            action(logItem.Exception, logItem.Message, new object[] { logItem.Request, logItem.Response });
        } 
        public void Log(string message)
        {
            Log(new LogItem(message));
        } 
        public void Log(string message, Exception exception)
        {
            Log(new LogItem(message, exception));
        }

        public void Log(string message, object request)
        {
            Log(new LogItem(message, request));
        }

        public void Log(string message, object request, Exception exception)
        {
            Log(new LogItem(message, request, exception));
        }
        public void Log(string message, object request, object response)
        {
            Log(new LogItem(message, request, response));
        }

        public void Log(string message, object request, object response, Dictionary<string, object> properties)
        {
            var item = new LogItem(message, request, response);
            item.AddProperties(properties);
            Log(item);
        }

        public void Log(string message, object request, object response, string key, object prop)
        {
            var item = new LogItem(message, request, response);
            item.AddProperties(key,prop);
            Log(item);
        }

        public void Log(string message, object request, object response, string key1, object prop1, string key2, object prop2)
        {
            var item = new LogItem(message, request, response);
            item.AddProperties(key1, prop1);
            item.AddProperties(key2, prop2);
            Log(item);
        }

        private void setLogAction(string logLevel)
        {
            switch (logLevel)
            {
                case LogLevelConstant.TRACE:
                    action = _logger.LogTrace;
                    break;
                case LogLevelConstant.DEBUG:
                    action = _logger.LogDebug;
                    break;
                case LogLevelConstant.INFORMATION:
                    action = _logger.LogInformation;
                    break;
                case LogLevelConstant.WARNING:
                    action = _logger.LogWarning;
                    break;
                case LogLevelConstant.ERROR:
                    action = _logger.LogError;
                    break;
                case LogLevelConstant.FATAL:
                    action = _logger.LogCritical;
                    break;
                default:
                    action = _logger.LogInformation;
                    break;
            }
        }
    }
}
