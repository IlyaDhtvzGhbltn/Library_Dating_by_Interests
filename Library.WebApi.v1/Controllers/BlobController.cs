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
    [AuthenticationFilter]
    [ApiController]
    [Route("api/v1")]
    public class BlobController : ControllerBase
    {
        private readonly IStorageService _storageService;
        private readonly IUserPhotosService _userPhotoService;

        public BlobController(IStorageService storageService, IUserPhotosService userPhotoService)
        {
            _storageService = storageService;
            _userPhotoService = userPhotoService;
        }

        [HttpPost]
        [Route("user/{userId}/uploadphoto")]
        public async Task<UploadPhotoResponse> UploadPhoto(
            [FromForm] IFormFile file,
            [FromRoute] string userId)
        {
            string fileUrl;
            using (var stream = file.OpenReadStream())
            {
                fileUrl = _storageService.SaveFile(stream, userId);
                Response.StatusCode = 201;
            }
            string photoId = _userPhotoService.SaveUserPhotoUrlDB(userId, new Uri(fileUrl));


            return new UploadPhotoResponse()
            {
                PhotoUri = new Uri(fileUrl),
                PhotoId = photoId
            };
        }

        [HttpDelete]
        [Route("user/{userId}/photo/{photoId}")]
        public async Task DeletePhoto(
            [FromRoute] string userId,
            [FromRoute] string photoId) 
        {
            bool deleteFromDB = _userPhotoService.DeletePhotoFromDB(photoId);
            bool deleteFromStorage = _storageService.DelteFile(photoId, userId);
            if (deleteFromDB && deleteFromStorage)
            {
                Response.StatusCode = 202;
            }
            else 
            {
                Response.StatusCode = 404;
            }
        }

        [HttpPatch]
        [Route("user/{userId}/setavatarphoto/{photoId}")]
        public async Task SetAsAvatar(
            [FromRoute] string userId,
            [FromRoute] string photoId)
        {
            bool photoChanged = _userPhotoService.SetPhotoAsAvatar(userId, photoId);
            if (photoChanged)
            {
                Response.StatusCode = 202;
            }
            else 
            {
                Response.StatusCode = 404;
            }
        }
    }

}
