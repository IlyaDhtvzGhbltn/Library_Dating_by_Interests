using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using System.Threading.Tasks;
using System;


namespace Library.Services
{
    public interface IDatingService : IService
    {
        public Task<Uri[]> EligibleProfiles(DatingCriteriaBase criteria);
        public Task<DatingProfile> EligibleProfile(string internalId);
        public Task ProfileReaction(string senderId, string profileId, Reaction reaction);
    }
}
