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
using Library.Contracts.MobileAndLibraryAPI.DTO;
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

        public async Task<DatingProfile> ViewProfile(Guid apiUserProfileId)
        {
            using (var context = _dbFactory.Create())
            {
                ApiUser profile = await _userData.FindUserById(context, apiUserProfileId, x => x.Photos);
                YouTubeSubscription[] commonSubs = context.ApiUserYoutubeChannel
                    .Where(x => x.ApiUser == profile)
                    .Include(x => x.YoutubeChanell)
                        .ThenInclude(x => x.Avatar)
                    .Select(x => new YouTubeSubscription
                    {
                        IconUrl = x.YoutubeChanell.Avatar.PhotoUrl,
                        Title = x.YoutubeChanell.YoutubeTitle
                    })
                    .ToArray();

                return new DatingProfile
                {
                    ApiUserId = apiUserProfileId,
                    CommonInfo = new CommonInfo
                    {
                        About = profile.About,
                        Age = profile.Age,
                        Gender = (Gender)profile.Gender,
                        Name = profile.UserName
                    },
                    Photos = profile.Photos
                        .Select(x => new DTOPhoto { Id = x.PhotoId, IsAvatar = x.IsAvatar, Uri = x.PhotoUrl })
                        .ToArray(),
                    CommonYouTubeSubscriptions = commonSubs
                };
            }
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

                    ApiUser user = await _userData.FindUserById(context, apiUserID, x => x.Photos);
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

        public async Task<RelationStatus> ReactionOnProfile(Guid requester, Guid responser, Reaction reaction)
        {
            using (var context = _dbFactory.Create())
            {
                UsersRelation relation = GetRelation(context, requester, responser);
                if (relation == null)
                {
                    await CreateUsersRelation(
                        context: context,
                        requester: requester,
                        responser: responser,
                        status: RelationStatus.ResponseAwaiting);

                    Guid dialogId = await CreateDialog(context, requester, responser);
                    ApiUser user = await this.FindUserById(context, requester);
                    var automaticMessage = new Message
                    {
                        Sender = user,
                        Text = $"{user.UserName} invited you into a chat.",
                        SendingTime = DateTime.UtcNow
                    };
                    AddMessageInDialog(context, automaticMessage, dialogId);
                }
                return RelationStatus.ResponseAwaiting;
            }
        }

        private void AddMessageInDialog(LibraryDatabaseContext context, Message automatic, Guid dialogId)
        {
            var dialog = context.Dialogs
                .Include(x => x.Messages)
                .First(x => x.Id == dialogId);
            dialog.Messages.Add(automatic);
            context.SaveChanges();
        }

        private async Task<Guid> CreateDialog(LibraryDatabaseContext context, Guid requester, Guid responser)
        {
            ApiUser requesterApiUser = await this.FindUserById(context, requester);
            ApiUser responserApiUser = await this.FindUserById(context, responser);
            var dialogModel = new Dialog
            {
                Creator = requester,
                Invited = responser,
                Participants = new List<ApiUser>()
            };
            dialogModel.Participants.Add(requesterApiUser);
            dialogModel.Participants.Add(responserApiUser);

            context.Dialogs.Add(dialogModel);
            context.SaveChanges();

            var dialog = await context.Dialogs.FirstAsync(x => x.Creator == requester && x.Invited == responser);
            return dialog.Id;
        }

        private async Task CreateUsersRelation(LibraryDatabaseContext context, Guid requester, Guid responser, RelationStatus status)
        {
            ApiUser req = await this.FindUserById(context, requester);
            ApiUser res = await this.FindUserById(context, responser);

            context.UsersRelations.Add(new UsersRelation 
            {
                Requester = req,
                Responser = res, 
                RelationStatus = status
            });
            context.SaveChanges();
        }

        private UsersRelation GetRelation(LibraryDatabaseContext context, Guid requester, Guid responser)
        {
            string requesterId = requester.ToString();
            string responserId = responser.ToString();
            UsersRelation relation = context.UsersRelations
                .Where(x => x.Requester.Id == requesterId && x.Responser.Id == responserId)
                .FirstOrDefault();
            return relation;
        }
    }
}
