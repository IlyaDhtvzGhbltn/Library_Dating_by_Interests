using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface ISignInService : IService
    {
        Task<SignInResponse> SignIn(SignInRequest request);
    }
}
