using Auction.Core.Base.Common.Interfaces.ServiceConfigurers;
using Auction.Core.Host.Service.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Auction.Core.Host.Service.HostExtensions
{
    public static class ServiceExtensionConfigurer
    {
        public static IServiceCollection FindConfigurersAndExecute(this IServiceCollection services)
        {
            var assemblies = AssemblyBuilderHelper.GetAssembliesFromDll();

            foreach (var assembly in assemblies)
            {

                var types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IServiceConfigurer)));

                if (!types.Any())
                {
                    continue;
                }

                types.ToList().ForEach(x =>
                {
                    //logger.LogWarning($"IServiceConfigurer reference found on {x.FullName} ");
                });


                foreach (var type in types)
                {
                    var configurer = (IServiceConfigurer)Activator.CreateInstance(type);
                    configurer.Configure(services);
                }
            }

            return services;
        }
    }
}
