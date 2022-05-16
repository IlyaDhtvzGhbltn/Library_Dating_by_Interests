using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IAuthorizationService
    {
        Task<bool> Authorize(string internalId, string bearerToken);
    }
}
