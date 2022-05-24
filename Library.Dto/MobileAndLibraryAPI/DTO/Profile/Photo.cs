using System;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Profile
{
    public class Photo
    {
        public bool IsAvatar { get; set; }
        public Guid Id { get; set; }
        public Uri Uri { get; set; }
    }
}