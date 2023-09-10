using System;
using System.Threading.Tasks;
using Super.Ticket.Persistence.Contexts;
using Super.Ticket.Persistence.Seeders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Super.Ticket.WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services  = scope.ServiceProvider; 

                try 
                {
                    var context = services.GetRequiredService<AppDbContext>();

                    if (context.Database.IsSqlServer())
                    {
                        await context.Database.MigrateAsync();
                        
                        var seeder = new AppDbContextSeeder();
                        seeder.SeedEverything(context);
                    }                    

                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }
            
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)                
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
