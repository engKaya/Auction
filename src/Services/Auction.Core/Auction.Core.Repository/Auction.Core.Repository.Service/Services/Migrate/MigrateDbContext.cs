using Auction.Core.Repository.Common.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace Auction.Core.Repository.Service.Services.Migrate
{

    public static class MigrateDbContext
    {
        public static void MigrateDatabase<TContext>(this IHost host, Type ContextType, Action<TContext, IServiceProvider>? seeder = null) where TContext : BaseDbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<BaseDbContext>>();
                var context = services.GetRequiredService(ContextType) as TContext;

                try
                {
                    logger.LogInformation($"Migrating Database associated with context db: {ContextType.Name}");

                    var retry = Policy.Handle<SqlException>()
                            .WaitAndRetry(new TimeSpan[]
                            {
                                TimeSpan.FromSeconds(3),
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(8),
                            });
                    retry.Execute(() => InvokeSeeder(seeder, context, services));

                    logger.LogInformation($"Migrated Database associated with context db: {ContextType.Name}");

                }
                catch (Exception ex)
                {
                    logger.LogError($"An Error Occured While Migrating {ContextType.Name}! Ex: {ex.Message}");
                }
            }
        }
        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider>? seeder, TContext context, IServiceProvider service)
            where TContext : BaseDbContext
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();
            if (seeder != null)
                seeder(context, service);
        }
    }
}
