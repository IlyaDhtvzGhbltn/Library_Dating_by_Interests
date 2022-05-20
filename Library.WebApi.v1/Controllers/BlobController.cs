using Library.Contracts.MobileAndLibraryAPI.RequestResponse.Profile.Photo;
using Library.Services;
using Library.WebApi.v1.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Controllers
{
    [AuthorizationFilter]
    [ApiController]
    [Route("api/v1/user")]
    public class BlobController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public BlobController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost]
        [Route("uploadphoto")]
        public async Task<UploadPhotoResponse> UploadPhoto([FromForm] IFormFile file)
        {
            return null;
        }
    }

}
