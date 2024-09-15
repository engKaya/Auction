using Auction.Core.Base.Common.Helpers;
using Auction.Core.Base.Common.Interfaces.HostConfigurer;
using Auction.Core.Host.Service.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Auction.Core.Host.Service.HostExtensions
{
    public static class HostBuilderHelper 
    {
        public static WebApplication ConfigureHost(this WebApplication app) 
        { 
            var assemblies = AssemblyBuilderHelper.GetAssembliesFromDll();

            foreach (var assembly in assemblies)
            {
                
                ConsoleHelper.WriteInfo($"Looking HostBuilder for Assembly: {assembly.GetName().Name}");

                var types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IHostConfigurer)));

                if (!types.Any())
                    continue;


                ConsoleHelper.WriteInfo($"{types.Count()} IHostConfigurer reference found");
                types.ToList().ForEach(x =>{ ConsoleHelper.WriteWarning($"IHostConfigurer reference found on {x.FullName}");});


                foreach (var type in types)
                {
                    var configurer = (IHostConfigurer)Activator.CreateInstance(type);
                    configurer.Configure(app);
                }
            }

            ConsoleHelper.WriteSuccess("FindHostConfigurersAndExecute has finished"); 
            return app;
        }
    }
}
