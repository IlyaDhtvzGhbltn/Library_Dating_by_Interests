using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using System.Threading.Tasks;
using Library.Services;
using System;
using System.Collections.Generic;

namespace Library.DummyServices
{
    public class YouTubeDummySignInService<TRequest, TResponse>
        : ISignInService<TRequest, TResponse>
        where TRequest : SignInRequest
        where TResponse : SignInResponse
    {
        private List<SignInResponse> _db = new List<SignInResponse>();

        public async Task<TResponse> SignIn(TRequest request)
        {
            string internalToken = Guid.NewGuid().ToString();
            string internalId = Guid.NewGuid().ToString();

            var responce = new SignInResponse() 
            {
                InternalToken = internalToken,
                InternalUserId = internalId
            };
            _db.Add(responce);

            
            return (TResponse)responce;
        }
    }
}