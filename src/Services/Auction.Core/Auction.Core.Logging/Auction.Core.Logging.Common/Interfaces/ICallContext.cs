namespace Auction.Core.Logging.Common.Interfaces
{
    public interface ICallContext
    {
        string ContextId { get; }
        /// <summary>
        /// Use with caution, this will generate a new context id
        /// </summary>
        ICallContext NewContextId();
        string GetIpAddress();
        string GetRequestPath();
        string GetUserId();
        string GetUserName();
    }
}
