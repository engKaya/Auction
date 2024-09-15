using Auction.Core.Base.Common.Infastructure;
using Auction.Core.Middleware.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace Auction.Core.Middleware.Service.Services.Middlewares
{
    public class RequestHandlerMiddleware : BaseMiddleware
    { 
        public RequestHandlerMiddleware(RequestDelegate next) : base(next)
        {
        }

        public override async Task Execute(HttpContext context)
        {
            var body = context.Request.Body;
            if (body != null && body.CanRead)
            {
                var request = await new StreamReader(body).ReadToEndAsync();
                if (!string.IsNullOrEmpty(request))
                {
                    var baseRequest = JsonConvert.DeserializeObject<BaseRequest>(request);
                    if (baseRequest != null)
                    {
                        baseRequest.RequestId = _callContext.GetContextId(); 
                        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(baseRequest));
                        context.Request.Body = new MemoryStream(bytes);
                    }

                }
            }

        }
    }
}
