using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using System.Threading.Tasks;
using System;


namespace Library.Services
{
    public interface IDatingService : IService
    {
        public Task<DatingProfile[]> EligibleProfiles(Guid apiUserProfileId, int skip, int geoKM, bool geoEnabled);
        public Task<RelationStatus> ReactionOnProfile(Guid requester, Guid responser, Reaction reaction);
        public Task<DatingProfile> ViewProfile(Guid apiUserProfileId);
    }
}
