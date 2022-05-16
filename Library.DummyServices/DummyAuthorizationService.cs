using System;
using Library.Services;
using System.Threading.Tasks;

namespace Library.DummyServices
{
    public class DummyAuthorizationService : IAuthorizationService
    {
        public async Task<bool> Authorize(string internalId, string bearerToken)
        {
            return true;
        }
    }
}
