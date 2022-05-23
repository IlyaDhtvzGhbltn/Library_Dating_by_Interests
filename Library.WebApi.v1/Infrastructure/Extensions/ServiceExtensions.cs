using Library.Contracts.Azure;
using Library.Entities;
using Library.Services;
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
        public static void ConfigureCoreServices(this IServiceCollection services)
        {
#if !Dummy

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