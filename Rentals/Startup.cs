using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rentals.Repositories;
using Rentals.Services;
using WebApiErrorHandling;

namespace Rentals
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
            ConfigureCustomServices(services);
            AddSwagger(services);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((doc, req) => doc.Servers.Add(new OpenApiServer
                {
                    Url = $"https://{req.Host.Value}/" + config.GetValue<string>("API_BASE_PATH")
                }));
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "Rentals");
            });

            app.UseHttpsRedirection();
            app.UseWebApiErrorHandling();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureCustomServices(IServiceCollection services)
        {
            services.AddScoped<IRentalsService, RentalsService>();
            services.AddScoped<IRentalsRepository, RentalsRepository>();
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Rentals",
                    Version = "v1",
                    Description = "Sample description"
                });
                c.EnableAnnotations();
                c.CustomSchemaIds(x => x.FullName);
            });
        }
    }
}
