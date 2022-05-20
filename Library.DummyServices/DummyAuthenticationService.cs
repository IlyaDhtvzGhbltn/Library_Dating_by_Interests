using System;
using Library.Services;
using System.Threading.Tasks;

namespace Library.DummyServices
{
    public class DummyAuthenticationService : IAuthenticationService
    {
        public async Task<bool> IsAuthenticated(string internalId, string bearerToken)
        {
            return true;
        }
    }
}
