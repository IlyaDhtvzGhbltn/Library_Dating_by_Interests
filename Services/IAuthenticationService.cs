using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IAuthenticationService<TRequest, TResponse> : IService
        where TRequest : IRequest
        where TResponse : IResponse
    {
        Task<TResponse> Auth(TRequest request);
    }
}
