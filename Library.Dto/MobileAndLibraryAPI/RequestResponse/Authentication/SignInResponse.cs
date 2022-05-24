using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication
{
    public class SignInResponse : IResponse
    {
        public string InternalJwt { get; set; }
    }
}
