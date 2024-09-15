using Microsoft.Extensions.Hosting;

namespace Auction.Core.Base.Common.AssemblyBuilders
{
    public abstract class CoreApplicationBuilder
    {
        public abstract void ApplicationBuilder(IHost application);
    }
}
