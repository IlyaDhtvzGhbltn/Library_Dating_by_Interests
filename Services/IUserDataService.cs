using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using System;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IUserDataService : IService
    {
        public Task<DatingCriteria> GetUserDatingCriteria(Guid apiUserId);
        Task<UserProfile> GetProfileByInternalId(Guid apiUserId);
        Task DeleteProfile(Guid apiUserId);
        Task ChangeUserCommonInfo(Guid apiUserId, CommonInfo info);
        Task ChangeUserDatingCriteria(Guid apiUserId, DatingCriteria criteria);
    }
}
