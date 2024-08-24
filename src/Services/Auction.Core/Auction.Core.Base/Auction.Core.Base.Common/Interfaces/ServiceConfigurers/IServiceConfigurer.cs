using Microsoft.Extensions.DependencyInjection;

namespace Auction.Core.Base.Common.Interfaces.ServiceConfigurers
{
    public interface IServiceConfigurer
    {
        void Configure(IServiceCollection services); 
    }
}
