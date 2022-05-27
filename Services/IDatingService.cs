using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using System.Threading.Tasks;
using System;


namespace Library.Services
{
    public interface IDatingService : IService
    {
        public Task<DatingProfile[]> EligibleProfiles(Guid apiUserId, int skip, int geoKM, bool geoEnabled);
        public Task<bool> ProfileReaction(string senderInternalId, string profileId, Reaction reaction);
    }
}
