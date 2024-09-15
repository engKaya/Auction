using Auction.Core.Host.Service.Helpers;
using Auction.Core.Host.Service.HostExtensions;
using Auction.Core.Repository.Common.Context;
using Auction.Core.Repository.Service.Services.Migrate;
using Microsoft.EntityFrameworkCore;

namespace Auction.Core.Host.Presentation
{
    public static class HostDesigner
    {
        public static WebApplication DesignHost(this WebApplication host)
        {
            host.
                MigrateDb().
                ConfigureHost();

            return host;
        }
        private static WebApplication MigrateDb(this WebApplication host)
        {
            var assemblies = AssemblyBuilderHelper.GetAssembliesFromDll().FindAll(x => x.FullName.Contains("Base") && !x.FullName.Contains("Core"));
            

            List<Type> ctxtTypes = [];

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(t => t.BaseType == typeof(BaseDbContext));
                if (!types.Any())
                    continue;
                 
                ctxtTypes.AddRange(types);
            }

            foreach (var ctxtType in ctxtTypes)
            {
                host.MigrateDatabase<BaseDbContext>(ctxtType);
            }

            return host;
        }
    }
}
