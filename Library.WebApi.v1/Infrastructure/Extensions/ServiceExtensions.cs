using Library.Contracts.Azure;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using Library.DummyServices;
using Library.Services;
using Library.WebApi.v1.Entities;
using Library.WebApi.v1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>();
            builder.AddEntityFrameworkStores<LibraryDatabaseContext>();
        }

        public static void ConfigureCoreServices(this IServiceCollection services)
        {
#if !Dummy
            //services.AddSingleton<IAuthenticationService<AuthenticateRequest, AuthenticateResponse>>
            //    (new YouTubeAuthenticationService());
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
    }
}