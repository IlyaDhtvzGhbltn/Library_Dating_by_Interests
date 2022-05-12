using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;


namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Profile
{
    public class ChangeUserProfileRequest : TrustedRequest
    {
        public CommonInfo CommonInfo { get; set; }
        public DatingCriteria DatingCriteria { get; set; }
    }
}
