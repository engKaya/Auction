using Auction.Core.Base.Common;
using Auction.Core.Base.Common.Helpers;
using Auction.Core.Base.Common.Interfaces.ServiceConfigurers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
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


            services.AddSerilog(cfg =>
            {
                cfg.MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .Enrich.WithProcessId()
                    .Enrich.WithProperty("ModuleName", moduleName)
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .WriteTo.File($"serilogs/log{DateTime.Now.Ticks}.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.Elasticsearch(ConfigureElasticSink(moduleName, config, environment))
                    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                    .ReadFrom.Configuration(config);
            });
        }


        private static ElasticsearchSinkOptions ConfigureElasticSink(string moduleName, IConfiguration configuration, string environment)
        {
            var uri = configuration["ElasticConfiguration:Uri"];
            string indexFormat = $"{moduleName.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";

            return new ElasticsearchSinkOptions(new Uri(uri))
            {
                AutoRegisterTemplate = true,
                IndexFormat = indexFormat.RemoveTurkishChars(),
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