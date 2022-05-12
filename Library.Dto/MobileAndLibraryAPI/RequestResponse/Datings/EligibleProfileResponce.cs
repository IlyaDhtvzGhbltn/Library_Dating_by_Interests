using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings
{
    public class EligibleProfileResponce : IResponse
    {
        public EligibleProfileResponce(DatingProfile profile)
        {
            Profile = profile;
        }
        public DatingProfile Profile { get; set; }
    }
}
