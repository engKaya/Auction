using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Auction.Core.Base.Common.Interfaces.HostConfigurer
{
    public interface IHostConfigurer
    {
        void Configure(IHost hostBuilder, ILogger logger);
    }
}
