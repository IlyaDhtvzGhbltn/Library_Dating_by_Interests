using Library.Contracts.MobileAndLibraryAPI.DTO;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Entities;
using Library.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using DatingCriteria = Library.Contracts.MobileAndLibraryAPI.DTO.Profile.DatingCriteria;
using Microsoft.EntityFrameworkCore;
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

        public Task ChangeUserCommonInfo(Guid internalId, CommonInfo info)
        {
            throw new NotImplementedException();
        }

        public Task ChangeUserDatingCriteria(Guid internalId, DatingCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProfile(Guid internalId)
        {
            throw new NotImplementedException();
        }

        public async Task<UserProfile> GetProfileByInternalId(Guid internalId)
        {
            using (var context = _dbFactory.Create())
            {
                string id = internalId.ToString();
                ApiUser user = context.LibraryUsers
                    .Include(x=> x.DatingCriterias)
                    .Include(x => x.Photos)
                    .Include(x=> x.Subscriptions).ThenInclude(y => y.Avatar)
                    .FirstOrDefault(x => x.Id == id);
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
                            All = user.DatingCriterias.IsGeo,
                            RadiusKm = user.DatingCriterias.GeoRadiusKm
                            },
                            MySubscriptions = user.Subscriptions.Select(x => x.Avatar.PhotoUrl.ToString()).ToArray()
                        }
                    };
                }
            }
        }
    }
}
