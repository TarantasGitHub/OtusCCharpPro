using ClassLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            if (configuration == null)
            {
                throw new Exception("Not found progect configuration.");
            }
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
            string? connectionString;

            connectionString = configuration?.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(ClassLibraryDependencyInjections).Assembly.GetName().Name;

            services.AddDbContext<WebDbContext>(
            options => options
            .UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );

            services.AddControllers();
            services.AddSwaggerGen();
            services.AddInfrastructure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}