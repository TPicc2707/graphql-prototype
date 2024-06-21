using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Address.API.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host,
                                                     Action<TContext, IServiceProvider> seeder, 
                                                     int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating database with context {DbContextName}", typeof(DbContext).Name);

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Migrated database with context {DbContextName}", typeof(DbContext).Name);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database using context {DbContextName}", typeof(DbContext).Name);

                    if(retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        Thread.Sleep(2000);
                        host.MigrateDatabase(seeder, retryForAvailability);
                    }

                    throw;
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, 
                                                   TContext context, IServiceProvider services) 
                                                   where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
