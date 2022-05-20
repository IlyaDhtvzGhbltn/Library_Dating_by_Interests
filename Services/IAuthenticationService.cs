using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IAuthenticationService
    {
        Task<bool> IsAuthenticated(string internalId, string bearerToken);
    }
}
