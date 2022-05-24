using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings
{
    public class EligibleProfilesRequest : ApiRequest
    {
        public DatingCriteria DatingCriterias { get; set; }
    }
}
