using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Profile
{
    public class CustomerProfile
    {
        public CommonInfo CommonInfo { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public DatingCriteria DatingCriterias { get; set; }
    }
}
