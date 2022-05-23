using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.YoutubeDTO
{
    public class YoutubeSubscriptionsResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public string prevPageToken { get; set; }
        public Pageinfo pageInfo { get; set; }
        public Item[] items { get; set; }
    }

    public class Resourceid
    {
        public string kind { get; set; }
        public string channelId { get; set; }
    }
}
