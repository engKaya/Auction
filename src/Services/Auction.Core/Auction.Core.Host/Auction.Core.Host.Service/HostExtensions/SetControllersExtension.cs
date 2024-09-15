using Auction.Core.Host.Service.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Auction.Core.Host.Service.HostExtensions
{
    public static class SetControllersExtension
    {
        public static IServiceCollection SetControllers(this IServiceCollection services)
        {
            var assemblies = AssemblyBuilderHelper.GetAssembliesFromDll().FindAll(x => x.FullName.Contains("Service") && !x.FullName.Contains("Core") && !x.FullName.Contains("Domain"));
            List<Assembly> serviceAssemblies  = new List<Assembly>();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes().Where(t => t.BaseType == typeof(ControllerBase));
                if (!types.Any())
                    continue;

                
                serviceAssemblies.Add(assembly);
            } 
            
            services.AddControllers()
                .ConfigureApplicationPartManager(x =>
                {
                    foreach (var item in assemblies)
                    {
                        var assembly = Assembly.Load(item.FullName);
                        var externalController = new AssemblyPart(assembly);
                        x.ApplicationParts.Add(externalController);
                    }
                });


            return services;
        }
    }
}
