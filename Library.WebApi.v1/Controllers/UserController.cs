using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Profile;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using System;

namespace Library.WebApi.v1.Controllers
{
    [Authorize(Roles = "Library.Entities.ApiUser")]
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IUserDataService _userDataService;
        private Guid _apiUserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public UserController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }


        [HttpGet]
        [Route("profile")]
        public async Task<IResponse> UserProfile()
        {
            UserProfile profile = await _userDataService.GetProfileByInternalId(_apiUserId);
            if (profile == null) 
            {
                Response.StatusCode = 404;
                return null;
            }
            var profileResponse = new UserProfileResponse(profile);
            return profileResponse;
        }

        [HttpPatch]
        [Route("profile")]
        public async Task ChangeUserProfile([FromBody]  ChangeUserProfileRequest request)
        {
            await _userDataService.ChangeUserCommonInfo(_apiUserId, request.CommonInfo);
            await _userDataService.ChangeUserDatingCriteria(_apiUserId, request.DatingCriteria);
        }

        [HttpDelete]
        [Route("profile")]
        public async Task DeleteProfile() 
        {
            await _userDataService.DeleteProfile(_apiUserId);
            this.HttpContext.Response.StatusCode = 202;
        }
    }
}
