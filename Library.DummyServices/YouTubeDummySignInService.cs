using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using System.Threading.Tasks;
using Library.Services;
using System;
using System.Collections.Generic;

namespace Library.DummyServices
{
    public class YouTubeDummySignInService : ISignInService
    {
        private List<SignInResponse> _db = new List<SignInResponse>();

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            string internalToken = Guid.NewGuid().ToString();

            var responce = new SignInResponse() 
            {
                InternalJwt = internalToken,
            };
            _db.Add(responce);
            
            return responce;
        }
    }
}