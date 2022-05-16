using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings;
using Library.Services;
using Library.WebApi.v1.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Controllers
{
    [ApiController]
    [AuthorizationFilter]
    [Route("api/v1/dating")]
    public class DatingController : ControllerBase
    {
        private IDatingService _datingService { get; set; }

        public DatingController(IDatingService datingService)
        {
            _datingService = datingService;
        }


        [HttpGet]
        [Route("user")]
        public async Task<IResponse> GetEligibleUsers(
            [FromQuery] DatingCriteria criterias)
        {
            Uri[] eligibleProfiles = await _datingService.EligibleProfiles(criterias);
            var resp = new EligibleProfilesResponce(eligibleProfiles);
            return resp;
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
