using Auction.Core.Base.Common.Assembly;
using Auction.Core.Host.Common.Infastructure;
using Auction.Core.Host.Service.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Auction.Core.Host.Service.HostExtensions
{
    public static class AssemblyBuilderExtension
    {
        public static IServiceCollection FindAssemblyBuildersAndExecute(this IServiceCollection services, ILogger logger)
        {
            logger.LogInformation("FindAssemblyBuildersAndExecute has started");

            var assemblies = AssemblyBuilderHelper.GetAssembliesFromDll(logger).Where(x=>x.FullName.Contains(".Service")).ToList();
            foreach (var assembly in assemblies)
            {
                logger.LogWarning($"Looking For ${assembly.FullName}");

                var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(AssemblyBuilder)) && !t.IsAbstract);
                 

                if (!types.Any())
                {
                    logger.LogInformation($"Any AssemblyBuilder reference couldn't found, continiuing");
                    continue;
                }

                logger.LogInformation($"{types.Count()} CoreAssemblyBuilder reference found");
                types.ToList().ForEach(x =>
                {
                    logger.LogWarning($"AssemblyBuilder reference found on {x.FullName} ");
                });


                foreach (var type in types)
                {
                    var configurer = (AssemblyBuilder)Activator.CreateInstance(type);
                    configurer.BuildFramework(services);
                }
            }
            return services;
        }
    }
}
