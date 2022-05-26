using Library.Contracts.MobileAndLibraryAPI.DTO;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Entities;
using Library.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.WebApi.v1.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using DatingCriteria = Library.Contracts.MobileAndLibraryAPI.DTO.Profile.DatingCriteria;
using UserPhoto = Library.Contracts.MobileAndLibraryAPI.DTO.Profile.Photo;

namespace Library.WebApi.v1.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly IFactory<LibraryDatabaseContext> _dbFactory;

        public UserDataService(IFactory<LibraryDatabaseContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }




        public async Task<UserProfile> GetProfileByInternalId(Guid internalId)
        {
            using (var context = _dbFactory.Create())
            {
                string id = internalId.ToString();
                ApiUser user = await context.ApiUsers
                    .Include(x => x.DatingCriterias)
                    .Include(x => x.Photos)
                    .Include(x => x.ApiUsers_YoutubeChannels)
                        .ThenInclude(y => y.YoutubeChanell)
                        .ThenInclude(y => y.Avatar)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return null;
                }
                else
                {
                    return new UserProfile()
                    {
                        CommonInfo = new CommonInfo()
                        {
                            About = user.About,
                            Age = user.Age,
                            Name = user.UserName,
                            Gender = (Gender)user.Gender
                        },
                        Photos = user.Photos.Select(x => new UserPhoto()
                        {
                            Uri = x.PhotoUrl,
                            IsAvatar = x.IsAvatar,
                            Id = x.PhotoId
                        })
                        .ToList(),
                        DatingCriterias = new DatingCriteria()
                        {
                            Age = new AgeCriteria()
                            {
                                MaxAge = user.DatingCriterias.MaxAge,
                                MinAge = user.DatingCriterias.MinAge,
                            },
                            Gender = new GenderCriteria()
                            {
                                Gender = (Gender)user.DatingCriterias.Gender
                            },
                            Geo = new GeoCriteria()
                            {
                                All = user.DatingCriterias.EnableGeoCriteria,
                                RadiusKm = user.DatingCriterias.GeoRadiusKm
                            },
                            MySubscriptions = user.ApiUsers_YoutubeChannels.Select(x => x.YoutubeChanell.Avatar.PhotoUrl.ToString()).ToArray()
                        }
                    };
                }
            }
        }

        public async Task ChangeUserCommonInfo(Guid internalId, CommonInfo info)
        {
            using (var context = _dbFactory.Create())
            {
                ApiUser user = await this.FindUserById(context, internalId);
                if (user == null) 
                {
                    throw new NullReferenceException();
                }

                user.UserName = info.Name;
                user.About = info.About;
                user.Age = info.Age;
                user.Gender = (int)info.Gender;

                await context.SaveChangesAsync();
            }
        }

        public async Task ChangeUserDatingCriteria(Guid internalId, DatingCriteria criteria)
        {
            using (var context = _dbFactory.Create())
            {
                var user = await this.FindUserById(context, internalId, "DatingCriterias");
                if (user == null)
                {
                    throw new NullReferenceException();
                }

                user.DatingCriterias.Gender = (int)criteria.Gender.Gender;
                user.DatingCriterias.GeoRadiusKm = criteria.Geo.RadiusKm;
                user.DatingCriterias.EnableGeoCriteria = criteria.Geo.All;
                user.DatingCriterias.MaxAge = criteria.Age.MaxAge;
                user.DatingCriterias.MinAge = criteria.Age.MinAge;

                await context.SaveChangesAsync();
            }
        }


        public async Task DeleteProfile(Guid internalId)
        {
            using (var context = _dbFactory.Create())
            {
                ApiUser user = await this.FindUserById(context, internalId, "Photos");
                if(user == null)
                { 
                    throw new NullReferenceException(); 
                }

                context.ApiUsers.Remove(user);
                context.Photos.RemoveRange(user.Photos);
                await context.SaveChangesAsync();
            }
        }

        public async Task<DatingCriteria> GetUserDatingCriteria(Guid apiUserId)
        {
            using (var context = _dbFactory.Create())
            {
                ApiUser user = await this.FindUserById(context, apiUserId, "DatingCriterias");
                return new DatingCriteria()
                {
                    Age = new AgeCriteria(
                        minAge: user.DatingCriterias.MinAge,
                        maxAge: user.DatingCriterias.MaxAge),
                    Gender = new GenderCriteria()
                    {
                        Gender = (Gender)user.DatingCriterias.Gender
                    },
                    Geo = new GeoCriteria()
                    {
                        All = user.DatingCriterias.EnableGeoCriteria,
                        RadiusKm = user.DatingCriterias.GeoRadiusKm
                    },
                };
            }
        }
    }
}
