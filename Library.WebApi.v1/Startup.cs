using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using Library.DummyServices;
using Library.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.DummyServices.DummyDto;

namespace Library.WebApi.v1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

#if DEBUG || Release
            services.AddSingleton<IAuthenticationService<AuthenticateRequest, AuthenticateResponse>>
                (new YouTubeAuthenticationService());
#elif Dummy
            services.AddSingleton<IAuthenticationService<AuthenticateRequest, AuthenticateResponse>>
                (new YouTubeDummyAuthenticationService<AuthenticateRequest, AuthenticateResponse>());
            services.AddSingleton<IUserDataService>
                (new DummyUserDataService());
            services.AddSingleton<IDatingService>
                (new DummyDatingService());
#endif
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
    }
}
