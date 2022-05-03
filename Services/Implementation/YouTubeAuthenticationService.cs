using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class YouTubeAuthenticationService
        : IAuthenticationService<AuthenticateRequest, AuthenticateResponse>
    {
        public Task<AuthenticateResponse> Auth(AuthenticateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
