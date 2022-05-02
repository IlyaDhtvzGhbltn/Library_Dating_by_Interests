using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using System.Collections.Generic;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Profile
{
    public class DatingCriteria
    {
        public AgeCriteria Age { get; set; }
        public GeoCriteria Geo { get; set; }
        public GenderCriteria Gender { get; set; }
        public ICollection<YouTubeSubscription> MySubscription { get; set; }
    }
}