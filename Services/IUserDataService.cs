using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using System;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IUserDataService : IService
    {
        Task<UserProfile> GetProfileByInternalId(Guid internalId);
        Task DeleteProfile(Guid internalId);
        Task ChangeUserCommonInfo(Guid internalId, CommonInfo info);
        Task ChangeUserDatingCriteria(Guid internalId, DatingCriteria criteria);
    }
}
