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

            services.AddSerilog(cfg =>
            {
                cfg.MinimumLevel.Information()
                    .WriteTo.Console()
                    .WriteTo.File($"log{DateTime.Now.Ticks}.txt", rollingInterval: RollingInterval.Day)
                    .Enrich.FromLogContext()
                    .WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(new Uri(config["ElasticConfiguration:Uri"] ?? "http://localhost:9200"))
                        {
                            IndexFormat = $"{config["ApplicationName"]?.ToLower().Replace(".", " - ")}- logs -{DateTime.UtcNow}",
                            AutoRegisterTemplate = true,
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
                            PipelineName = "pipeline",
                        })
                    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                    .ReadFrom.Configuration(config);
            });
        }
    }
}
