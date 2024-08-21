﻿using Auction.Core.Host.Service.HostExtensions;

namespace Auction.Core.Host.Presentation
{
    public static class HostServiceDesigners
    {
        public static void ConfigureHostServices(this IServiceCollection services, ILogger logger)
        {
            services
                .ConfigureLogging()
                .FindConfigurersAndExecute(logger)
                .FindAssemblyBuildersAndExecute(logger)
                .SetControllers(logger);
        } 

        private static IServiceCollection ConfigureLogging(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            }).Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug);
            var llog = services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
            return services;
        }
    }
}