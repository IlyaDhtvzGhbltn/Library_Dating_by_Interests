using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication
{
    public class SignInRequest : IRequest
    {
        public string ExternalToken { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
