using Library.Contracts;
using Library.Contracts.Azure;
using Library.Entities;
using Library.Services;
using Library.WebApi.v1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
#if !Dummy
            IFactory<LibraryDatabaseContext> factory = CreateFactoryDBContext(configuration);
            services.AddSingleton<IUserDataService>(new UserDataService(factory));
            services.AddSingleton<IDatingService>(new DatingService(factory));
#elif Dummy
            services.AddSingleton<ISignInService<SignInRequest, SignInResponse>>
                (new YouTubeDummySignInService<SignInRequest, SignInResponse>());
            services.AddSingleton<IUserDataService>(new DummyUserDataService());
            services.AddSingleton<IDatingService>(new DummyDatingService());
            services.AddSingleton<IDialogService>(new DummyDialogService());
            services.AddSingleton<IUserPhotosService>(new DummyUserPhotosService());
#endif        
        }

        public static void ConfigureAzure(this IServiceCollection services, AzureBlobStorageOptions options)
        {
            services.AddSingleton<IStorageService>(new BlobStorageService(options));
        }

        public static void AddAndConfigureAuthentication(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddAuthentication(
                x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    byte[] key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY"));
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration[AppSettings.JWT.JwtIssuer],
                        ValidAudience = configuration[AppSettings.JWT.JwtAudience],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
        }

        public static void ConfigureDBContext(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<LibraryDatabaseContext>(options =>
                options.UseSqlServer(configuration[AppSettings.ConnectionString]));

            services.AddSingleton<IFactory<LibraryDatabaseContext>>(CreateFactoryDBContext(configuration));
        }

        private static IFactory<LibraryDatabaseContext> CreateFactoryDBContext(IConfiguration configuration) 
        {
            return new DbFactory<LibraryDatabaseContext>(() =>
            {
                var optBuilder = new DbContextOptionsBuilder<LibraryDatabaseContext>();
                optBuilder.UseSqlServer(configuration[AppSettings.ConnectionString]);
                optBuilder.EnableSensitiveDataLogging();
                return new LibraryDatabaseContext(optBuilder.Options);
            });
        }
    }
}