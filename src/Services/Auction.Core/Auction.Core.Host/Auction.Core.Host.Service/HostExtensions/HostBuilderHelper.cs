using Auction.Core.Base.Common.Interfaces.HostConfigurer;
using Auction.Core.Host.Service.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Auction.Core.Host.Service.HostExtensions
{
    public static class HostBuilderHelper 
    {
        public static IHost ConfigureHost(this IHost host) 
        { 
            var assemblies = AssemblyBuilderHelper.GetAssembliesFromDll();

            foreach (var assembly in assemblies)
            {
                //logger.LogWarning($"Looking For ${assembly.FullName}");

                var types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IHostConfigurer)));

                if (!types.Any())
                    continue;

                //logger.LogInformation($"{types.Count()} IHostConfigurer reference found");
                types.ToList().ForEach(x =>
                {
                    //logger.LogWarning($"IHostConfigurer reference found on {x.FullName} ");
                });


                foreach (var type in types)
                {
                    var configurer = (IHostConfigurer)Activator.CreateInstance(type);
                    configurer.Configure(host);
                }
            }

            //logger.LogInformation("FindHostConfigurersAndExecute has finished");
            return host;
        }
    }
}
