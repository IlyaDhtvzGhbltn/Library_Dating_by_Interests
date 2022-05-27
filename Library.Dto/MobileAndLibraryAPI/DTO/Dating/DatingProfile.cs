using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using System;
using System.Collections.Generic;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dating
{
    public class DatingProfile
    {
        public DatingProfile()
        {
            CommonInfo = new CommonInfo();
        }

        public Guid ApiUserId { get; set; }
        public CommonInfo CommonInfo { get; set; }
        public Photo[] Photos { get; set; }
        public YouTubeSubscription[] CommonYouTubeSubscriptions { get; set; }
    }
}