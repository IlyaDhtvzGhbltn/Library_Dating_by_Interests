using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Entities;
using Library.Services;
using System;
using DatingCriteria = Library.Contracts.MobileAndLibraryAPI.DTO.Profile.DatingCriteria;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.WebApi.v1.Services
{
    public class DatingService : IDatingService
    {
        private readonly IFactory<LibraryDatabaseContext> _dbFactory;

        public DatingService(IFactory<LibraryDatabaseContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }


        public Task<DatingProfile> EligibleProfile(string apiUserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ProfileReaction(string senderInternalId, string profileId, Reaction reaction)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> EligibleProfilesId(Guid apiUserId, int skip)
        {
            using (var context = _dbFactory.Create())
            {
                var ress = context.ApiUsers.FromSqlRaw("GetUsersWithSameSubscriptions", apiUserId)
                    .Select(x => x.Id)
                    .ToArray();
            }
            return null;
        }
    }
}
