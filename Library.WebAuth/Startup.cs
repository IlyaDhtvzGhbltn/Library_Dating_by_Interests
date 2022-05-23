using Library.Contracts;
using Library.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Library.Auth
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
            //necessary for adding migrations and database updating via PM console 
            services.AddDbContext<LibraryDatabaseContext>(options =>
                options.UseSqlServer(Configuration[AppSettings.ConnectionString]));

            services.AddSingleton<IFactory<LibraryDatabaseContext>>(new DbFactory<LibraryDatabaseContext>(() =>
            {
                var optBuilder = new DbContextOptionsBuilder<LibraryDatabaseContext>();
                optBuilder.UseSqlServer(Configuration[AppSettings.ConnectionString]);
                optBuilder.EnableSensitiveDataLogging();
                return new LibraryDatabaseContext(optBuilder.Options);
            }));


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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
