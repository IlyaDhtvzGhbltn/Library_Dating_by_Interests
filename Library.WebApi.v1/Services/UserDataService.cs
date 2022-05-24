using Library.Contracts.MobileAndLibraryAPI.DTO;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Entities;
using Library.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using DatingCriteria = Library.Contracts.MobileAndLibraryAPI.DTO.Profile.DatingCriteria;
using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Microsoft.EntityFrameworkCore;

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
                    .Include(x=>x.DatingCriterias)
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
                             }
                         }
                    };
                }
            }
        }
    }
}
