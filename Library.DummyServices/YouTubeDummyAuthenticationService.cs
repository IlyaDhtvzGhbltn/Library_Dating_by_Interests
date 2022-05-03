using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using System.Threading.Tasks;
using Library.Services;
using System;
using System.Collections.Generic;

namespace Library.DummyServices
{
    public class YouTubeDummyAuthenticationService<TRequest, TResponse>
        : IAuthenticationService<TRequest, TResponse>
        where TRequest : AuthenticateRequest
        where TResponse : AuthenticateResponse
    {
        private List<AuthenticateResponse> _db = new List<AuthenticateResponse>();

        public async Task<TResponse> Auth(TRequest request)
        {
            string internalToken = Guid.NewGuid().ToString();
            string internalId = Guid.NewGuid().ToString();

            var responce = new AuthenticateResponse() 
            {
                InternalToken = internalToken,
                InternalUserId = internalId
            };
            _db.Add(responce);

            return (TResponse)responce;
        }
    }
}