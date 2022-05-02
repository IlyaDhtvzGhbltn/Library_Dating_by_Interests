using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using System.Collections.Generic;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dating
{
    public class DatingProfile
    {
        public CommonInfo CommonInfo { get; set; }
        public Photo[] Photos { get; set; }
        public YouTubeSubscription[] YouTubeSubscriptions { get; set; }
    }
}