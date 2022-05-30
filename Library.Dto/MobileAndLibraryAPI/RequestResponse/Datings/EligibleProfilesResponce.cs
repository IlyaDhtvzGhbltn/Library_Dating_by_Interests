using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings
{
    public class EligibleProfilesResponce : IResponse
    {
        public EligibleProfilesResponce()
        {}

        public EligibleProfilesResponce(DatingProfile[] profile)
        {
            Profiles = profile;
        }
        public DatingProfile[] Profiles { get; set; }
    }
}
