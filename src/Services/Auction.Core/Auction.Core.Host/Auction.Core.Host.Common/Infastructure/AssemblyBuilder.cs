using Auction.Core.Base.Common.AssemblyBuilders;
using Auction.Core.Repository.Common.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.Core.Host.Common.Infastructure
{
    public abstract class AssemblyBuilder : CoreAssemblyBuilder
    {
        private IServiceCollection _services;

        public IServiceCollection BuildFramework(IServiceCollection services)
        {
            _services = services;
            SetDbContext();
            BuildServices(_services);
            return _services;
        }
        protected abstract void SetDbContext();
        public void SetDbContext<C>() where C : BaseDbContext
        {
            _services.AddDbContext<C>(x =>
            {
                x.UseSqlServer("Data Source=DESKTOP-FJARE0U;Initial Catalog=AUCTION_USER;Persist Security Info=True;TrustServerCertificate=True;User ID=bid_service;Password=app") 
                .EnableDetailedErrors(false)
                .EnableSensitiveDataLogging(false);

            }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);
        }
    }
}
