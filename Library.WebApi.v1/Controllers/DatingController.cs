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
        public async Task<IResponse> Search([FromQuery]int skip = 0)
        {
            int apiUserKm = await _userDaraService.FindApiUserGeoKm(_apiUserId);
            bool apiUserGeoEnabled = await _userDaraService.FindApiUserGeoEnabled(_apiUserId);
            DatingProfile[] profilesId = await _datingService.EligibleProfiles(_apiUserId, skip, apiUserKm, apiUserGeoEnabled);
            return new EligibleProfilesResponce(profilesId);
        }


        [HttpPatch]
        [Route("reaction_on/{profileId}")]
        public async Task<IResponse> Reaction(
            [FromRoute]string profileId,
            [FromBody]ReactionRequest reactionRequest) 
        {
            Reaction reaction = reactionRequest.Reaction;
            bool ok = await _datingService.ProfileReaction(_apiUserId.ToString(), profileId, reaction);
            if (ok)
            {
                HttpContext.Response.StatusCode = 200;
            }
            else 
            {
                HttpContext.Response.StatusCode = 404;
            }
            return null;
        }
    }
}
