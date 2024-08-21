using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Auction.Core.Base.Common.Interfaces.ServiceConfigurers
{
    public interface IServiceConfigurer
    {
        void Configure(IServiceCollection services, ILogger logger); 
    }
}
