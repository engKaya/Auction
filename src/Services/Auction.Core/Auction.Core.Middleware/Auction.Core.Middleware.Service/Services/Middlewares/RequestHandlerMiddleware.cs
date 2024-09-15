using Auction.Core.Base.Common.Constants;
using Auction.Core.Base.Common.Infastructure;
using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Middleware.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IO;
using Newtonsoft.Json;
using System.Text;

namespace Auction.Core.Middleware.Service.Services.Middlewares
{
    public class RequestHandlerMiddleware : BaseMiddleware
    {
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager; 
        public RequestHandlerMiddleware(RequestDelegate next) : base(next)
        {
            _recyclableMemoryStreamManager =    new RecyclableMemoryStreamManager(new RecyclableMemoryStreamManager.Options() { 
                 AggressiveBufferReturn = true,
                 LargeBufferMultiple    = 2,
                 BlockSize = 1024,
                 MaximumBufferSize = 1024 * 64,
                 MaximumLargePoolFreeBytes = 1024 * 1024,
                 MaximumSmallPoolFreeBytes = 1024 * 1024,
                 UseExponentialLargeBuffer = true,
            }); 
        }

        public override async Task Execute(HttpContext context)
        {
            try
            { 
                var controllerActionDescriptor = context
                    .GetEndpoint()
                    .Metadata
                     .GetMetadata<ControllerActionDescriptor>();

                var parameters = controllerActionDescriptor.Parameters;

                parameters.ToList().ForEach(async parameter =>
                {
                    if (parameter.ParameterType.BaseType == typeof(BaseRequest))
                    {
                        var request = context.Request;
                        var body = request.Body;
                        var buffer = _recyclableMemoryStreamManager.GetStream();
                        await body.CopyToAsync(buffer);
                        buffer.Position = 0;
                        var bodyAsText = await new StreamReader(buffer).ReadToEndAsync();
                        var rawRequest = JsonConvert.DeserializeObject(bodyAsText, parameter.ParameterType);
                        if (rawRequest != null)
                        {
                            BaseRequest baseRequest  = (BaseRequest)rawRequest;
                            baseRequest.RequestId = _callContext.ContextId;
                            context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(baseRequest)));
                            context.Request.ContentLength = context.Request.Body.Length;
                        }
                    }
                });
            }
            catch (Exception)
            {
                _trace.Log($"Error On RequestHandlerMiddleware");
            }
        }
    }
}
