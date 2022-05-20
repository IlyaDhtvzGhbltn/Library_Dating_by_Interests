using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Profile.Photo
{
    public class UploadPhotoResponse : IResponse
    {
        public string PhotoId { get; set; }
        public Uri PhotoUri { get; set; }
    }
}
