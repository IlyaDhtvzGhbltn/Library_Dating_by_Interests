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

namespace Library.WebApi.v1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddMvc();
            services.AddMvcCore();

            AzureBlobStorageOptions options = GetAzureOptions();
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
            services.AddSingleton<IStorageService>(new BlobStorageService(options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseSwagger();
        }

        private AzureBlobStorageOptions GetAzureOptions() 
        {
            string blobKey = Configuration.GetSection(AppSettings.BlobStorageKey).Value;
            string connectionString = Configuration.GetSection(AppSettings.BlobStorageConnectionString).Value;
            string accountName = Configuration.GetSection(AppSettings.BlobStorageAccountName).Value;
            string containerName = Configuration.GetSection(AppSettings.BlobStorageContainerName).Value;
            string blobURL = Configuration.GetSection(AppSettings.BlobURL).Value;

            var options = new AzureBlobStorageOptions()
            {
                AccountKey = blobKey,
                ConnectionString = connectionString,
                AccountName = accountName,
                ContainerName = containerName, 
                BlobUrl = blobURL
            };

            return options;
        }
    }
}
