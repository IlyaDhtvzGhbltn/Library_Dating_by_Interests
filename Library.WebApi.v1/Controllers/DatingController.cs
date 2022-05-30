using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings;
using Library.Entities;
using Library.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Controllers
{
    [ApiController]
    [Route("api/v1/dating")]
    [Authorize(Roles = "Library.Entities.ApiUser")]
    public class DatingController : ControllerBase
    {
        private readonly IDatingService _datingService;
        private readonly IUserDataService _userDaraService;
        private Guid _apiUserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


        public DatingController(IDatingService datingService, IUserDataService userDaraService)
        {
            _datingService = datingService;
            _userDaraService = userDaraService;
        }


        [HttpGet]
        [Route("search")]
        public async Task<IResponse> Search([FromQuery] int skip = 0)
        {
            int apiUserKm = await _userDaraService.FindApiUserGeoKm(_apiUserId);
            bool apiUserGeoEnabled = await _userDaraService.FindApiUserGeoEnabled(_apiUserId);
            DatingProfile[] profilesId = await _datingService.EligibleProfiles(_apiUserId, skip, apiUserKm, apiUserGeoEnabled);
            return new EligibleProfilesResponce(profilesId);
        }

        /// <summary>
        /// Method required for viewing profile from dialogs.
        /// Doesn't return common subscriptions but ALL profile subs instead
        /// </summary>
        /// <param name="apiUserProfileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("view_profile/{apiUserProfileId}")]
        public async Task<IResponse> View([FromRoute] Guid apiUserProfileId)
        {
            DatingProfile profile = await _datingService.ViewProfile(apiUserProfileId);
            return new EligibleProfileResponce(profile);
        }


        [HttpPatch]
        [Route("reaction_on/{apiUserProfileId}")]
        public async Task<IResponse> Reaction(
            [FromRoute]Guid apiUserProfileId,
            [FromBody]ReactionRequest reactionRequest) 
        {
            Reaction reaction = reactionRequest.Reaction;
            RelationStatus status = await _datingService.ReactionOnProfile(
                requester:_apiUserId, 
                responser: apiUserProfileId, 
                reaction: reaction);

            return new ReactionResponse((int)status);
        }
    }
}
