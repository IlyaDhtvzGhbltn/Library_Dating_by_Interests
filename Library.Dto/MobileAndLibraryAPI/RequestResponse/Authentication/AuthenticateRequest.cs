using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication
{
    public class AuthenticateRequest : IRequest
    {
        public string ExternalId { get; set; }
        public string Token { get; set; }
    }
}
