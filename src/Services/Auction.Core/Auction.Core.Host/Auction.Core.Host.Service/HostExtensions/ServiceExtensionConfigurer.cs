using Auction.Core.Base.Common.Interfaces.ServiceConfigurers;
using Auction.Core.Host.Service.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Auction.Core.Host.Service.HostExtensions
{
    public static class ServiceExtensionConfigurer
    {
        public static IServiceCollection FindConfigurersAndExecute(this IServiceCollection services, ILogger logger)
        {
            logger.LogInformation("FindConfigurersAndExecute has started");
            var assemblies = AssemblyBuilderHelper.GetAssembliesFromDll(logger);

            foreach (var assembly in assemblies)
            {
                logger.LogWarning($"Looking For ${assembly.FullName}");

                var types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IServiceConfigurer)));

                if (!types.Any())
                {
                    logger.LogInformation($"Any IServiceConfigurer reference couldn't found, continiuing");
                    continue;
                }

                logger.LogInformation($"{types.Count()} IServiceConfigurer reference found");
                types.ToList().ForEach(x =>
                {
                    logger.LogWarning($"IServiceConfigurer reference found on {x.FullName} ");
                });


                foreach (var type in types)
                {
                    var configurer = (IServiceConfigurer)Activator.CreateInstance(type);
                    configurer.Configure(services, logger);
                }
            }

            logger.LogInformation("FindConfigurersAndExecute has finished");
            return services;
        }
    }
}
