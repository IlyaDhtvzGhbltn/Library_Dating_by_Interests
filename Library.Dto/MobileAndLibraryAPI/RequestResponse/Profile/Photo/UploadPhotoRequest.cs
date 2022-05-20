using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Profile.Photo
{
    public class UploadPhotoRequest : IRequest
    {
        public IFormFile File { get; set; }
    }
}
