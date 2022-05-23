using Library.Contracts;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using Library.Contracts.YoutubeDTO;
using Library.Entities;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Auth.Controllers
{

    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IFactory<LibraryDatabaseContext> _dbFactory;
        private readonly IConfiguration _config;


        public AuthController(
            IFactory<LibraryDatabaseContext> factory, 
            IConfiguration config)
        {
            _dbFactory = factory;
            _config = config;

        }


        [HttpPost]
        [Route("auth/youtube")]
        public async Task<SignInResponse> SignInViaYouTube(SignInRequest request)
        {
            YoutubeProfile profile = await GetYoutubeProfile(request.ExternalToken);
            if (profile == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else 
            {
                ApiUser user = await GetApiUser(profile.items[0].id);
                if (user == null) 
                {
                    user = await RegisterProfileUser(profile, request.ExternalToken);
                }
                string internalJwt = GenerateJwt(user);
                return new SignInResponse() { InternalJwt = internalJwt };
            }
        }

        private string GenerateJwt(ApiUser user)
        {
            string secretKeyValue = Environment.GetEnvironmentVariable("KEY");
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyValue));
            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.YoutubeUserName)
            };

            var token = new JwtSecurityToken(
                issuer: _config[AppSettings.JWT.JwtIssuer],
                audience: _config[AppSettings.JWT.JwtAudience],
                claims: claims, 
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ApiUser> RegisterProfileUser(YoutubeProfile profile, string externalToken)
        {
            using (var context = _dbFactory.Create())
            {
                Item item = profile.items[0];

                var user = new ApiUser()
                {
                    YoutubeUserId = item.id,
                    YoutubeUserName = item.snippet.title
                };

                var photos = new List<Photo>();
                photos.Add(new Photo() 
                {
                    IsAvatar = true,
                    PhotoId = Guid.NewGuid(),
                    PhotoUrl = new Uri(item.snippet.thumbnails.high.url)
                });

                user.Photos = photos;
                ICollection<YoutubeSubscriptionsResponse> userSubs = await GetUserSubscriptions(externalToken);
                user.Subscriptions = new List<YoutubeChanell>();
                foreach (var subscriptionResponce in userSubs) 
                {
                    foreach (var youtubeItem in subscriptionResponce.items) 
                    {
                        YoutubeChanell channell = await SaveChannellIfNotExist(youtubeItem);
                        user.Subscriptions.Add(channell);
                    }
                }
                context.LibraryUsers.Add(user);
                context.SaveChanges();
                return context.LibraryUsers.First(x => x.YoutubeUserId == item.id);
            }
        }

        private async Task<ICollection<YoutubeSubscriptionsResponse>> GetUserSubscriptions(string externalToken)
        {
            string subsUrl = string.Format(_config[AppSettings.Youtube.SubscriptionsUrl], externalToken);

            ICollection<YoutubeSubscriptionsResponse> subsCollection = new List<YoutubeSubscriptionsResponse>();
            var subs = await GetResponce<YoutubeSubscriptionsResponse>(subsUrl);

            while (!string.IsNullOrWhiteSpace(subs.nextPageToken))
            {
                subsCollection.Add(subs);
                string nextPageToken = subs.nextPageToken;
                string nextSubsPageUrl = string.Format(_config[AppSettings.Youtube.SubscriptionsNextPageUrl], externalToken, nextPageToken);
                subs = await GetResponce<YoutubeSubscriptionsResponse>(nextSubsPageUrl);
            }
            subsCollection.Add(subs);

            return subsCollection;
        }

        private async Task<ApiUser> GetApiUser(string id)
        {
            using (var context = _dbFactory.Create()) 
            {
                ApiUser user = await context.LibraryUsers.FirstOrDefaultAsync(x => x.YoutubeUserId == id);
                return user;
            }
        }

        private async Task<YoutubeProfile> GetYoutubeProfile(string externalToken)
        {
            string url = string.Format(_config[AppSettings.Youtube.ProfileInfoUrl], externalToken);
            YoutubeProfile profile = await GetResponce<YoutubeProfile>(url);
            return profile;
        }

        private async Task<T> GetResponce<T>(string url) 
        {
            using (var _client = new HttpClient())
            {
                using (HttpResponseMessage response = await _client.GetAsync(url))
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    T obj = JsonConvert.DeserializeObject<T>(responseBody);
                    return obj;
                }
            }
        }

        private async Task<YoutubeChanell> SaveChannellIfNotExist(Item channel) 
        {
            using (var context = _dbFactory.Create())
            {
                var existedChannel = await context.YoutubeChanells
                    .FirstOrDefaultAsync(x => x.YoutubeId == channel.id);
                if (existedChannel != null)
                {
                    return existedChannel;
                }
                else 
                {
                    context.YoutubeChanells.Add(new YoutubeChanell() 
                    {
                        YoutubeId = channel.id, 
                        YoutubeDescription = channel.snippet.description,
                        YoutubeTitle = channel.snippet.title,
                        Avatar = new Photo() 
                        {
                            IsAvatar =true, 
                            PhotoUrl = new Uri(channel.snippet.thumbnails.high.url) 
                        }                 
                    });
                    context.SaveChanges();
                    return await context.YoutubeChanells.FirstAsync(x => x.YoutubeId == channel.id);
                }
            }
        }
    }
}
