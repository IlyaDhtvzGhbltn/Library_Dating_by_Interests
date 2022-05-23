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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.IdentityModel.Tokens;
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

            services.AddDbContext<LibraryDatabaseContext>(options => 
                options.UseSqlServer(Configuration[AppSettings.ConnectionString]));

            services.AddSingleton<IFactory<LibraryDatabaseContext>>(new DbFactory<LibraryDatabaseContext>(() =>
            {
                var optBuilder = new DbContextOptionsBuilder<LibraryDatabaseContext>();
                optBuilder.UseSqlServer(Configuration[AppSettings.ConnectionString]);
                optBuilder.EnableSensitiveDataLogging();
                return new LibraryDatabaseContext(optBuilder.Options);
            }));

            services.AddAuthentication(
                x=> 
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o => 
                {
                    byte[] key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY"));
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration[AppSettings.JWT.JwtIssuer],
                        ValidAudience = Configuration[AppSettings.JWT.JwtAudience],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

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
