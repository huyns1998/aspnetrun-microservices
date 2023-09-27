using Discount.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Discount.API.Data
{
    public class DiscountDbContextSeed
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DiscountDbContext>());
            }
        }

        private static void SeedData(DiscountDbContext context)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }


            if (!context.Coupons.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                var coupons = GetPreconfiguredCoupons();

                foreach (var item in coupons)
                {
                    context.Coupons.Add(item);
                }

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }

        private static IEnumerable<Coupon> GetPreconfiguredCoupons()
        {
            return new List<Coupon>()
            {
                new Coupon()
                {
                    ProductName = "IPhone X",
                    Description = "IPhone Discount",
                    Amount= 150,
                },
                new Coupon()
                {
                    ProductName = "Samsung 10",
                    Description = "IPhone Discount",
                    Amount= 100,
                }
            };
        }
    }
}
