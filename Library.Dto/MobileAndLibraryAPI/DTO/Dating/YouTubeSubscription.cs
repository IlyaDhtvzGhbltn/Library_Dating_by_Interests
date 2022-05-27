using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dating
{
    public class YouTubeSubscription
    {
        public string ChannelId { get; set; }
        public Uri IconUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
