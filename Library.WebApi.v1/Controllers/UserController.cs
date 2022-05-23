using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Profile;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IUserDataService _userDataService;

        public UserController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }


        [HttpGet]
        [Route("profile/{internalId}")]
        public async Task<IResponse> UserProfile([FromRoute] string internalId)
        {
            UserProfile profile = await _userDataService.GetProfileByInternalId(internalId);
            var profileResponse = new UserProfileResponse(profile);
            return profileResponse;
        }

        [HttpPatch]
        [Route("profile/{internalId}")]
        public async Task ChangeUserProfile(
            [FromRoute] string internalId, 
            [FromBody]  ChangeUserProfileRequest request)
        {
            await _userDataService.ChangeUserCommonInfo(internalId, request.CommonInfo);
            await _userDataService.ChangeUserDatingCriteria(internalId, request.DatingCriteria);
        }

        [HttpDelete]
        [Route("profile/{internalId}")]
        public async Task DeleteProfile(
            [FromRoute] string internalId) 
        {
            await _userDataService.DeleteProfile(internalId);
            this.HttpContext.Response.StatusCode = 202;
        }
    }
}
