using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ISignInService<TRequest, TResponse> : IService
        where TRequest : IRequest
        where TResponse : IResponse
    {
        Task<TResponse> SignIn(TRequest request);
    }
}
