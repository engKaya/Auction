using Auction.Core.Base.Common;
using Auction.Core.Base.Common.Enums;
using Auction.Core.Base.Common.Extensions;
using Auction.Core.Base.Common.Helpers;
using Auction.Core.Base.Common.Interfaces.ServiceConfigurers;
using Auction.Core.Logging.Common.Interfaces;
using Auction.Core.Logging.Service.Enrichers;
using Auction.Core.Logging.Service.Infastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Auction.Core.Logging.Service
{
    internal class LoggingServiceConfigurer : IServiceConfigurer
    {
        public void Configure(IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var moduleName = Environment.GetEnvironmentVariable(EnviromentConstants.MODULE_NAME);
            var environment = Environment.GetEnvironmentVariable(EnviromentConstants.ENVIRONMENT);

            services.AddScoped<ICallContext, CallContext>(); 
            services.AddScoped<ITraceService, TraceService>();
            services.AddScoped<RequestIdEnricher>();

            var enricher = services.BuildServiceProvider().GetRequiredService<RequestIdEnricher>();

            services.AddSerilog((Action<LoggerConfiguration>)(cfg =>
            {
                cfg.MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .Enrich.WithProcessId()
                    .Enrich.WithProperty("ModuleName", moduleName)
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Elasticsearch(ConfigureElasticSink(moduleName, config, environment))
                    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                    .Enrich.With(enricher)
                    .ReadFrom.Configuration(config);

                if (ApplicationHelper.GetEnvironmentType() != EnvironmentType.Development)
                    cfg.WriteTo.File($"serilogs/log{DateTime.Now.Ticks}.txt", rollingInterval: RollingInterval.Day);
            }));
        }


        private static ElasticsearchSinkOptions ConfigureElasticSink(string moduleName, IConfiguration configuration, string environment)
        {
            var uri = configuration["ElasticConfiguration:Uri"];
            string indexFormat = $"{moduleName.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";

            return new ElasticsearchSinkOptions(new Uri(uri))
            {
                AutoRegisterTemplate = true,
                IndexFormat = indexFormat.RemoveTurkishChars(),
                InlineFields = true,
                DetectElasticsearchVersion = true,
                ConnectionTimeout = TimeSpan.FromSeconds(20),
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                NumberOfShards = 2,
                NumberOfReplicas = 1,
                FailureCallback = (l, ex) => Console.WriteLine($"Unable to submit event:{l}|Exception: {ex}"),
                EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                                               EmitEventFailureHandling.WriteToFailureSink |
                                                               EmitEventFailureHandling.RaiseCallback,
                CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
                DeadLetterIndexName = "deadletterindex",
                RegisterTemplateFailure = RegisterTemplateRecovery.IndexAnyway,
            };
        }
    }
}