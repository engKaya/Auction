using Auction.Core.Logging.Common.Classes;

namespace Auction.Core.Logging.Common.Interfaces
{
    public interface ITraceService
    {
        void Log(LogItem logItem); 
        void Log(string message);
        void Log(string message, Exception exception);
        void Log(string message, object request, Exception exception);
        void Log(string message, object request);
        void Log(string message, object request, object response); 
        void Log(string message, object request, object response, Dictionary<string, object> properties); 
        void Log(string message, object request, object response, string key, object prop); 
        void Log(string message, object request, object response, string key1, object prop1, string key2, object prop2); 

    }
}
