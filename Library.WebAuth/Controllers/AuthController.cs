using Library.Contracts;
using Library.Contracts.MobileAndLibraryAPI.DTO;
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
            if (profile == null && profile.items == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else 
            {
                ApiUser user = await GetApiUser(profile.items[0].id);
                if (user == null) 
                {
                    user = await RegisterProfileUser(profile, request);
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
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, typeof(ApiUser).ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config[AppSettings.JWT.JwtIssuer],
                audience: _config[AppSettings.JWT.JwtAudience],
                claims: claims, 
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ApiUser> RegisterProfileUser(YoutubeProfile profile, SignInRequest request)
        {
            using (var context = _dbFactory.Create())
            {
                ApiUser apiUser = await SaveApiUser(context, request, profile.items[0]);
                List<YoutubeChanell> channels = await SaveYoutubeChannels(context, request.ExternalToken);

                await SaveMaping(context, apiUser, channels);
                return apiUser;
            }
        }

        private async Task SaveMaping(LibraryDatabaseContext context, ApiUser apiUser, List<YoutubeChanell> channels)
        {
            var apiUsers_YoutubeChannels = new List<ApiUser_YoutubeChannel>();
            foreach (var channel in channels) 
            {
                apiUsers_YoutubeChannels.Add(new ApiUser_YoutubeChannel() 
                {
                    ApiUser = apiUser, 
                    ApiUserId = apiUser.Id, 
                    YoutubeChanell = channel, 
                    YoutubeChannelId = channel.Id
                });
            }
            context.ApiUserYoutubeChannel.AddRange(apiUsers_YoutubeChannels);
            context.SaveChanges();
        }

        private async Task<List<YoutubeChanell>> SaveYoutubeChannels(LibraryDatabaseContext context, string externalToken)
        {
            var subsList = await GetUserSubscriptions(externalToken);
            var channelsUserWatching = new List<YoutubeChanell>();
            foreach (var listItem in subsList)
            {
                foreach (var youtubeItem in listItem.items)
                {
                    YoutubeChanell channell = await FindChannel(context, youtubeItem);
                    if (channell == null)
                    {
                        channell = await CreateChannel(context, youtubeItem);
                    }
                    channelsUserWatching.Add(channell);
                }
            }
            return channelsUserWatching;
        }

        private async Task<ApiUser> SaveApiUser(LibraryDatabaseContext context, SignInRequest request, Item item)
        {
            ApiUser user = CreateApiUserModel(request, item);
            context.ApiUsers.Add(user);
            context.SaveChanges();
            return await context.ApiUsers.FirstAsync(x => x.YoutubeUserId == item.id);
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
                ApiUser user = await context.ApiUsers.FirstOrDefaultAsync(x => x.YoutubeUserId == id);
                return user;
            }
        }

        private async Task<YoutubeProfile> GetYoutubeProfile(string externalToken)
        {
            string url = string.Format(_config[AppSettings.Youtube.ProfileInfoUrl], externalToken);
            YoutubeProfile profile = await GetResponce<YoutubeProfile>(url);
            return profile;
        }

        private async Task<YoutubeChanell> CreateChannel(LibraryDatabaseContext context, Item item) 
        {
            var channel = new YoutubeChanell()
            {
                YoutubeId = item.id,
                YoutubeDescription = item.snippet.description,
                YoutubeTitle = item.snippet.title,
                Avatar = new Photo()
                {
                    IsAvatar = true,
                    PhotoUrl = new Uri(item.snippet.thumbnails.high.url)
                }
            };

            context.YoutubeChanells.Add(channel);
            context.SaveChanges();
            var savedChannel = await context.YoutubeChanells.FirstAsync(x => x.YoutubeId == item.id);
            return savedChannel;
        }

        private async Task<YoutubeChanell> FindChannel(LibraryDatabaseContext context, Item channel) 
        {
                var channell = await context.YoutubeChanells
                    .FirstOrDefaultAsync(x => x.YoutubeId == channel.id);
                return channell;
        }

        private ApiUser CreateApiUserModel(SignInRequest request, Item item) 
        {
            var user = new ApiUser()
            {
                YoutubeUserId = item.id,
                UserName = item.snippet.title,
                About = item.snippet.description,
                Gender = (int)Gender.Unknown,
                Age = 18,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                DatingCriterias = new DatingCriteriaEntry()
                {
                    Gender = (int)Gender.Unknown,
                    GeoRadiusKm = 15,
                    EnableGeoCriteria = false,
                    MinAge = 18,
                    MaxAge = 30,
                }
            };

            var photos = new List<Photo>();
            photos.Add(new Photo()
            {
                IsAvatar = true,
                PhotoId = Guid.NewGuid(),
                PhotoUrl = new Uri(item.snippet.thumbnails.high.url)
            });

            user.Photos = photos;
            return user;
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
    }
}
