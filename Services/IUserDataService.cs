using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IUserDataService : IService
    {
        Task<UserProfile> GetProfileByInternalId(string internalId);
        Task DeleteProfile(string internalId);
        Task ChangeUserCommonInfo(string internalId, CommonInfo info);
        Task ChangeUserDatingCriteria(string internalId, DatingCriteria criteria);
    }
}
