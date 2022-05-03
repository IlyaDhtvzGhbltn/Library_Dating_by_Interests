﻿using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Authentication;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Profile;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService<AuthenticateRequest, AuthenticateResponse> _authenticationService;
        private readonly IUserDataService _userDataService;

        public UserController(
            IAuthenticationService<AuthenticateRequest, AuthenticateResponse> authenticationService,
            IUserDataService userDataService)
        {
            _userDataService = userDataService;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("auth/youtube")]
        public async Task<IResponse> AuthenticationViaYouTube(AuthenticateRequest request)
        {
            AuthenticateResponse response = await _authenticationService.Auth(request);
            return response;
        }


        [HttpGet]
        [Route("profile/{internalId}")]
        public async Task<IResponse> UserProfile(
            [FromRoute] string internalId,
            [FromHeader] string internalBearerToken)
        {
            UserProfile profile = await _userDataService.GetUserProfile(internalId);
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
            [FromRoute] string internalId, 
            [FromBody] TrustedRequest request) 
        {
            await _userDataService.DeleteProfile(internalId);
            this.HttpContext.Response.StatusCode = 202;
        }
    }
}
