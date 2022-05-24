using Library.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Library.WebApi.v1.Infrastructure.Extensions;
using System;
using Library.Contracts;
using System.Text;
using Library.Entities;

namespace Library.WebApi.v1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDBContext(Configuration);

            services.AddAndConfigureAuthentication(Configuration);
            services.AddAuthorization();

            services.ConfigureCoreServices(Configuration);
            services.ConfigureAzure(Configuration.GetAzureOptions());

            services.AddSwaggerGen();
            services.AddControllers();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
