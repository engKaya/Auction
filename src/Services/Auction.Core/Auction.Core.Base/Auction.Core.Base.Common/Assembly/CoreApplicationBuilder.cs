using Microsoft.Extensions.Hosting;

namespace Auction.Core.Base.Common.Assembly
{
    public abstract class CoreApplicationBuilder
    {
        public abstract void ApplicationBuilder(IHost application);
    }
}
