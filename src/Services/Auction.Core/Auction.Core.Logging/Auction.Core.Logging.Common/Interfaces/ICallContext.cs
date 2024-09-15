namespace Auction.Core.Logging.Common.Interfaces
{
    public interface ICallContext
    {
        string GetContextId();
        string GetUserId();
        string GetUserName();
        string GetIpAddress();
        string GetRequestPath();
    }
}
