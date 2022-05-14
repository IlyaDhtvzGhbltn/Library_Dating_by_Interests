using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using System.Collections.Generic;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Profile
{
    public class UserProfile
    {
        public UserProfile()
        {
            CommonInfo = new CommonInfo();
            DatingCriterias = new DatingCriteria();

            DatingCriterias.Age = new AgeCriteria();
            DatingCriterias.Geo = new GeoCriteria();
            DatingCriterias.Gender = new GenderCriteria();
        }

        public CommonInfo CommonInfo { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public DatingCriteria DatingCriterias { get; set; }
    }
}
