using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Entities;
using Library.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Infrastructure.Extensions
{
    public static class IServiceExtensions
    {
        public static async Task<ApiUser> FindUserById(this IService service, LibraryDatabaseContext context, Guid guid) 
        {
            string id = guid.ToString();
            ApiUser user = await context.ApiUsers.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
       
        
        public static async Task<ApiUser> FindUserById<T>(this IService service, 
            LibraryDatabaseContext context, 
            Guid guid,
            Expression<Func<ApiUser, T>> include)
        {
            string id = guid.ToString();

            ApiUser user = await context.ApiUsers
                .Include(include)
                .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public static async Task<YouTubeSubscription> FindYouTubeSubscription(this IService service, LibraryDatabaseContext context, string channelId) 
        {
            Guid channelGuid = Guid.Parse(channelId);
            YoutubeChanell channel = await context.YoutubeChanells
                .Include(x => x.Avatar)
                .FirstOrDefaultAsync(x => x.Id == channelGuid);
            if (channel == null)
                return null;
            return new YouTubeSubscription 
            {
                ChannelId = channelId, 
                Description = channel.YoutubeDescription, 
                IconUrl = channel.Avatar.PhotoUrl, 
                Title = channel.YoutubeTitle
            };
        }
    }
}
