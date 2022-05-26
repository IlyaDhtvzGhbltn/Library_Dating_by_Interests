using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings
{
    public class EligibleProfilesResponce : IResponse
    {
        public EligibleProfilesResponce(string[] ids)
        {
            IDs = ids;
        }
        public string[] IDs { get; set; }
    }
}
