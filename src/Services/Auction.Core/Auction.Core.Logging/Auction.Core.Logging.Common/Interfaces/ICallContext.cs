namespace Auction.Core.Logging.Common.Interfaces
{
    public interface ICallContext
    {  
        string ContextId { get; }
        string GetIpAddress();
        string GetRequestPath();
        string GetUserId();
        string GetUserName();
    }
}
