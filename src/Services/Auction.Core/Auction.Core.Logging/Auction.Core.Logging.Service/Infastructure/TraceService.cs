using Auction.Core.Base.Common;
using Auction.Core.Base.Common.Constants;
using Auction.Core.Logging.Common.Classes;
using Auction.Core.Logging.Common.Constants;
using Auction.Core.Logging.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace Auction.Core.Logging.Service.Infastructure
{
    public class TraceService : ITraceService
    {
        ILogger<LogItem> _logger;
        private readonly string RequestId;
        private Action<Exception?, string?, object[]> action;


        public TraceService(ILogger<LogItem> logger, IHttpContextAccessor accessor)
        {
            _logger = logger;
            RequestId = accessor.HttpContext.Request.Headers[HttpHeaderConstants.XRequestId].ToString() ?? Guid.NewGuid().ToString();
            setLogAction(Environment.GetEnvironmentVariable(EnviromentConstants.LOG_LEVEL));
        }
        public void Log(LogItem logItem)
        {
            var logProperties = new Dictionary<string, object>
            {
                { "RequestId", RequestId },
                { "ModuleName", Environment.GetEnvironmentVariable(EnviromentConstants.MODULE_NAME) },
                { "Environment", Environment.GetEnvironmentVariable(EnviromentConstants.ENVIRONMENT) }
            };

            if (logItem.Properties != null)
            {
                foreach (var prop in logItem.Properties)
                {
                    logProperties.Add(prop.Key, prop.Value);
                }
            }

            action(logItem.Exception, logItem.Message, new object[] { logItem.Request, logItem.Response, logProperties });
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
