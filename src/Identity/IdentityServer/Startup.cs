using IdentityServer.Data;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _configuration= configuration;
            _environment= environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connString = _configuration.GetConnectionString("IdentityServer");
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connString));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddConfigurationStore(opt =>
                {
                    opt.ConfigureDbContext = builder => builder.UseSqlServer(connString,
                        opt => opt.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(opt =>
                {
                    opt.ConfigureDbContext = builder => builder.UseSqlServer(connString,
                        opt => opt.MigrationsAssembly(migrationAssembly));
                })
                //.AddInMemoryClients(Config.Clients)
                //.AddInMemoryIdentityResources(Config.IdentityResources)
                //.AddInMemoryApiResources(Config.ApiResources)
                //.AddInMemoryApiScopes(Config.ApiScopes)
                //.AddTestUsers(TestUsers.Users)
                .AddDeveloperSigningCredential()
                .AddProfileService<CustomProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();   
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapDefaultControllerRoute();   
            });
        }
    }
}
