namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dating
{
    public class DatingCriteriaBase
    {
        public AgeCriteria Age { get; set; }
        public GeoCriteria Geo { get; set; }
        public GenderCriteria Gender { get; set; }
        public string[] MySubscriptions { get; set; }
    }
}
