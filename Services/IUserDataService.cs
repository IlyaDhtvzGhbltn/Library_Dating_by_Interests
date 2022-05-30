using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Entities;
using System;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IUserDataService : IService
    {
        Task<int> FindApiUserGeoKm(Guid apiUserId);
        Task<bool> FindApiUserGeoEnabled(Guid apiUserId);
        Task<DatingCriteria> GetUserDatingCriteria(Guid apiUserId);
        Task<UserProfile> GetProfileByInternalId(Guid apiUserId);
        Task DeleteProfile(Guid apiUserId);
        Task ChangeUserCommonInfo(Guid apiUserId, CommonInfo info);
        Task ChangeUserDatingCriteria(Guid apiUserId, DatingCriteria criteria);
    }
}
