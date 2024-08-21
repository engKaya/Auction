using Auction.Core.Host.Common.Infastructure;
using Auction.User.Base.DbContext;
using Auction.User.Common.Infastructure.Repository;
using Auction.User.Common.Infastructure.UnitOfWork;
using Auction.User.Domain.Service.Infastructure.Repository;
using Auction.User.Domain.Service.Infastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace Auction.User.Service
{
    public class AssemblyBuild : AssemblyBuilder
    {
        public override void BuildServices(IServiceCollection services)
        {
            services.AddScoped<IUserServiceUnitOfWork, UserServiceUnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        protected override void SetDbContext() => SetDbContext<UserDbContext>();
    }
}
