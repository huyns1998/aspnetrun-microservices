using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Persistence;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Extensions
{
    public class OrderingSeed
    {
        public static async Task PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<OrderContext>();
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }

                if (!context.Orders.Any())
                {
                    Console.WriteLine("--> Seeding Data...");

                    await OrderContextSeed.SeedAsync(context);

                }
                else
                {
                    Console.WriteLine("--> We already have data");
                }
            }
        }
    }
}
