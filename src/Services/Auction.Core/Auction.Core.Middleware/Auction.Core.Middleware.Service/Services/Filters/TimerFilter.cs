using Auction.Core.Logging.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Auction.Core.Middleware.Service.Services.Filters
{
    public class TimerFilter : ActionFilterAttribute
    {
        private Stopwatch _timer;
        private readonly ITraceService _trace;
        public TimerFilter(ITraceService trace)
        {
            _trace = trace;
        }
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            _timer = Stopwatch.StartNew();
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            _trace.Log($"Action {context.ActionDescriptor.DisplayName} executed in {_timer.ElapsedMilliseconds} ms");
        }
    }
}
