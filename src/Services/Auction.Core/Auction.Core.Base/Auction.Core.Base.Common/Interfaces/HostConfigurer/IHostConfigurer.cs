using Microsoft.AspNetCore.Builder;

namespace Auction.Core.Base.Common.Interfaces.HostConfigurer
{
    public interface IHostConfigurer
    {
        void Configure(WebApplication hostBuilder);
    }
}
