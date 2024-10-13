using Auction.Core.Host.Service.HostExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Auction.Core.Host.Presentation
{
    public static class HostServiceDesigners
    {
        public static void ConfigureHostServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
                    options.JsonSerializerOptions.IgnoreNullValues = false;
                    options.JsonSerializerOptions.WriteIndented = true;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;

                });

            services
                .FindConfigurersAndExecute()
                .FindAssemblyBuildersAndExecute()
                .SetControllers();
        }

        //private static IServiceCollection ConfigureLogging(this IServiceCollection services)
        //{
        //    services.AddLogging(builder =>
        //    {
        //        builder.AddConsole();
        //        builder.AddDebug();
        //    }).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug);
        //    var llog = services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
        //    return services;
        //}
    }
}
