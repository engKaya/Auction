using Auction.Core.Base.Common.Enums;

namespace Auction.Core.Base.Common.Infastructure
{
    public class BaseRequest
    {
        public RequestTypes RequestTypes { get; set; } = RequestTypes.None;
        public DateTime RequestTime { get; set; } = DateTime.Now;
        public string RequestId { get; set; } = string.Empty;
    }
}
