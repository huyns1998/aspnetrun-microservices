using Catalog.API.AuthrizationHandler;
using Catalog.API.BL;
using Catalog.API.Data;
using Catalog.API.GrpcServices;
using Catalog.API.Protos;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Catalog.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatalogDbContext>(opt =>
                    opt.UseSqlServer(Configuration.GetConnectionString("CatalogConn")));

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductService, ProductService>();

            services.AddGrpcClient<DiscountProtoservice.DiscountProtoserviceClient>
                (o => o.Address = new Uri(Configuration["GrpcSettings:DiscountUrl"]));
            services.AddScoped<DiscountGrpcService>();

            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" });
            });

            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(opt =>
                    {
                        opt.ApiName = "catalogapi";
                        opt.Authority = "https://localhost:5015";
                    });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("ReadCatalogPolicy", policy =>
                {
                    policy.RequireClaim("client_id", "catalogClient", "mvc_client");
                    policy.Requirements.Add(new ScopeRequirement("catalogapi.read"));
                    //policy.RequireRole("User");
                });
            });

            services.AddSingleton<IAuthorizationHandler, ScopeHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            CatalogContextSeed.PrepPopulation(app);
        }
    }
}
