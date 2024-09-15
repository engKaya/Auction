using Auction.Core.Base.Common.Constants;
using Auction.Core.Logging.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Auction.Core.Logging.Service.Infastructure
{
    public sealed class CallContext : ICallContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CallContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        private string _contextId = string.Empty;
        public string ContextId
        {
            get
            {
                if (string.IsNullOrEmpty(_contextId))
                {
                    _contextId = getContextId();
                }
                return _contextId;
            }
        }

        private string getContextId()
        {
            var contextId = _contextAccessor.HttpContext?.Request.Headers[HttpHeaderConstants.XRequestId].ToString()  ??  _contextAccessor.HttpContext?.TraceIdentifier ;
            return string.IsNullOrEmpty(contextId) ? "No Request Id Available" : contextId;
        }

        public string GetIpAddress()
        {
            throw new NotImplementedException();
        }

        public string GetRequestPath()
        {
            throw new NotImplementedException();
        }

        public string GetUserId()
        {
            throw new NotImplementedException();
        }

        public string GetUserName()
        {
            throw new NotImplementedException();
        }
    }
}
