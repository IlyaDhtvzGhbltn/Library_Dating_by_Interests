using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings
{
    public class EligibleProfilesResponce : IResponse
    {
        public EligibleProfilesResponce(Uri[] uris)
        {
            UsersProfiles = uris;
        }
        public Uri[] UsersProfiles { get; set; }
    }
}
