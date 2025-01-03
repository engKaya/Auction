﻿using Auction.Core.Base.Common.Constants;
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
            var contextId = _contextAccessor.HttpContext?.Request.Headers[HttpHeaderConstants.XRequestId].ToString() ?? _contextAccessor.HttpContext?.TraceIdentifier;
            return string.IsNullOrEmpty(contextId) ? Guid.NewGuid().ToString() : contextId;
        }

        public string GetIpAddress() => _contextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0"; 

        public string GetRequestPath() => _contextAccessor.HttpContext?.Request.Path ?? string.Empty;

        public string GetUserId()
        {
            throw new NotImplementedException();
        }

        public string GetUserName()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Use with caution, this will generate a new context id
        /// </summary>
        public ICallContext NewContextId()
        {
            _contextId = Guid.NewGuid().ToString();
            return this;
        }
    }
}
