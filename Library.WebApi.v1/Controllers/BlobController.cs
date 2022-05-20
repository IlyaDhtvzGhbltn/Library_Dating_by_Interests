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
            string fileUrl;
            using (var stream = file.OpenReadStream())
            {
                fileUrl = _storageService.SaveFile(stream);
                Response.StatusCode = 201;
            }
            return new UploadPhotoResponse()
            {
                PhotoUri = new Uri(fileUrl)
            };
        }
    }

}
