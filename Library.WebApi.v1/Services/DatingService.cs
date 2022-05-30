using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Entities;
using Library.Services;
using System;
using DTOPhoto = Library.Contracts.MobileAndLibraryAPI.DTO.Profile.Photo;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.WebApi.v1.Infrastructure.Extensions;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using System.Collections.Generic;

namespace Library.WebApi.v1.Services
{
    public class DatingService : IDatingService
    {
        private readonly IFactory<LibraryDatabaseContext> _dbFactory;
        private readonly IUserDataService _userData;

        public DatingService(IFactory<LibraryDatabaseContext> dbFactory, IUserDataService userData)
        {
            _dbFactory = dbFactory;
            _userData = userData;
        }



        //TODO move skip and take functions into database
        //TODO move geo functions into database
        public async Task<DatingProfile[]> EligibleProfiles(Guid apiUserId, int skip, int geoKM, bool geoEnabled)
        {
            string command = $"EXEC GetUsersWithSameSubscription '{apiUserId}'";
            using (var context = _dbFactory.Create())
            {
                var bindings = context.ApiUserYoutubeChannel.FromSqlRaw(command)
                    .AsEnumerable()
                    .GroupBy(x => x.ApiUserId)
                    .ToArray();

                var profiles = new DatingProfile[bindings.Length];
                for (int i = 0; i < profiles.Length; i++)
                {
                    Guid apiUserID = Guid.Parse(bindings[i].Key);
                    ApiUser user = await _userData.FindUserById(context, apiUserID, "Photos");
                    DTOPhoto[] photos = user.Photos
                        .Select(x => new DTOPhoto { Id = x.PhotoId, IsAvatar = x.IsAvatar, Uri = x.PhotoUrl})
                        .ToArray();

                    var list = bindings[i].ToArray();

                    var subscriptions = new YouTubeSubscription[list.Length];
                    for(int j=0; j < list.Length; j++) 
                    {
                        string subsId = list[j].YoutubeChannelId.ToString();
                        YouTubeSubscription subscript = await this.FindYouTubeSubscription(context, subsId);
                        subscriptions[j] = subscript; 
                    };

                    profiles[i] = new DatingProfile()
                    {
                        ApiUserId = apiUserID,
                        CommonInfo = new CommonInfo()
                        {
                            About = user.About,
                            Age = user.Age,
                            Name = user.UserName,
                            Gender = (Contracts.MobileAndLibraryAPI.DTO.Gender)user.Gender
                        },
                        CommonYouTubeSubscriptions = subscriptions,
                        Photos = photos
                    };
                }
                return profiles;
            }
        }

        public Task<bool> ProfileReaction(string senderInternalId, string profileId, Reaction reaction)
        {
            throw new NotImplementedException();
        }


        private class SubscriptionsMaping 
        {
            public string ApiUserId { get; set; }
            public string[] ApiUserSubsId { get; set; }
        }
    }
}
