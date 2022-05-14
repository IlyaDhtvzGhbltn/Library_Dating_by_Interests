using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Profile

{
    public class UserProfileResponse : IResponse
    {
        public UserProfileResponse()
        {

        }
        public UserProfileResponse(UserProfile profile)
        {
            Profile = profile;
        }

        public UserProfile Profile { get; set; }
    }
}
