using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Controllers
{
    [ApiController]
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
            [FromHeader] string internalBearerToken,
            [FromQuery] DatingCriteria criterias)
        {
            Uri[] eligibleProfiles = await _datingService.EligibleProfiles(criterias);
            var resp = new EligibleProfilesResponce(eligibleProfiles);
            return resp;
        }


        [HttpGet]
        [Route("user/{eligibleProfileId}")]
        public async Task<IResponse> GetEligibleUser(
            [FromRoute]  string eligibleProfileId, 
            [FromHeader] string internalBearerToken)
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
        [Route("user/{senderId}/reaction_on/{profileId}")]
        public async Task<IResponse> Reaction(
            [FromRoute]string senderId,
            [FromRoute]string profileId,
            [FromBody]ReactionRequest reactionRequest) 
        {
            Reaction reaction = reactionRequest.Reaction;
            await _datingService.ProfileReaction(senderId, profileId, reaction);
            return null;
        }
    }
}
