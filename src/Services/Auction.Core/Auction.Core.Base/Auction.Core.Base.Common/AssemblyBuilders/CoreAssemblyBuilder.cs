using Microsoft.Extensions.DependencyInjection;

namespace Auction.Core.Base.Common.AssemblyBuilders
{
    public abstract class CoreAssemblyBuilder
    {
        public abstract void BuildServices(IServiceCollection services);
    }
}
