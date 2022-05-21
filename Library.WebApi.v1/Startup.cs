using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using Library.DummyServices;
using Library.Services;
using Library.WebApi.v1.Services;
using Library.WebApi.v1.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Library.Contracts.Azure;
using Microsoft.EntityFrameworkCore;
using Library.WebApi.v1.Infrastructure.Extensions;

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

            services.AddDbContext<LibraryDatabaseContext>(options => 
                options.UseSqlServer(Configuration.GetSection(AppSettings.ConnectionString).Value));

            services.AddAuthentication();
            services.ConfigureIdentity();
            services.AddAuthorization();

            services.ConfigureCoreServices();
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
