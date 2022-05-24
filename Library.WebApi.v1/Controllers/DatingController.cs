using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings;
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
        private Guid _apiUserId => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


        public DatingController(IDatingService datingService)
        {
            _datingService = datingService;
        }


        [HttpGet]
        [Route("search")]
        public async Task<IResponse> GetEligibleUsers([FromQuery]int skip = 0)
        {
            DatingCriteria criteria = await _datingService.GetUserDatingCriteria(_apiUserId);
            return null;
        }


        [HttpGet]
        [Route("user/{eligibleProfileId}")]
        public async Task<IResponse> GetEligibleUser(
            [FromRoute]  string eligibleProfileId)
        {
            DatingProfile profile = await _datingService.EligibleProfile(eligibleProfileId);
            if (profile != null)
            {
                var resp = new EligibleProfileResponce(profile);
                return resp;
            }
            else 
            {
                this.HttpContext.Response.StatusCode = 404;
                return null;
            }
        }


        [HttpPatch]
        [Route("user/{senderInternalId}/reaction_on/{profileId}")]
        public async Task<IResponse> Reaction(
            [FromRoute]string senderInternalId,
            [FromRoute]string profileId,
            [FromBody]ReactionRequest reactionRequest) 
        {
            Reaction reaction = reactionRequest.Reaction;
            bool ok = await _datingService.ProfileReaction(senderInternalId, profileId, reaction);
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
