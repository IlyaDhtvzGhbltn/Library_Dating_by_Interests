using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Profile;
using Library.Services;
using Library.WebApi.v1.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly ISignInService<SignInRequest, SignInResponse> _authenticationService;
        private readonly IUserDataService _userDataService;

        public UserController(
            ISignInService<SignInRequest, SignInResponse> authenticationService,
            IUserDataService userDataService)
        {
            _userDataService = userDataService;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("auth/youtube")]
        public async Task<IResponse> AuthenticationViaYouTube(SignInRequest request)
        {
            SignInResponse response = await _authenticationService.SignIn(request);
            return response;
        }


        [HttpGet]
        [AuthenticationFilter]
        [Route("profile/{internalId}")]
        public async Task<IResponse> UserProfile([FromRoute] string internalId)
        {
            UserProfile profile = await _userDataService.GetProfileByInternalId(internalId);
            var profileResponse = new UserProfileResponse(profile);
            return profileResponse;
        }

        [HttpPatch]
        [AuthenticationFilter]
        [Route("profile/{internalId}")]
        public async Task ChangeUserProfile(
            [FromRoute] string internalId, 
            [FromBody]  ChangeUserProfileRequest request)
        {
            await _userDataService.ChangeUserCommonInfo(internalId, request.CommonInfo);
            await _userDataService.ChangeUserDatingCriteria(internalId, request.DatingCriteria);
        }

        [HttpDelete]
        [AuthenticationFilter]
        [Route("profile/{internalId}")]
        public async Task DeleteProfile(
            [FromRoute] string internalId) 
        {
            await _userDataService.DeleteProfile(internalId);
            this.HttpContext.Response.StatusCode = 202;
        }
    }
}
