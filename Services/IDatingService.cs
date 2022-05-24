using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using System.Threading.Tasks;
using System;


namespace Library.Services
{
    public interface IDatingService : IService
    {
        public Task<DatingCriteria> GetUserDatingCriteria(Guid apiUserId);
        public Task<string[]> EligibleProfiles(DatingCriteria criteria, int skip);
        public Task<DatingProfile> EligibleProfile(string apiUserId);
        public Task<bool> ProfileReaction(string senderInternalId, string profileId, Reaction reaction);
    }
}
