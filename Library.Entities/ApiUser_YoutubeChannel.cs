using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Entities
{
    public class ApiUser_YoutubeChannel
    {
        public string ApiUserId { get; set; }
        public Guid YoutubeChannelId { get; set; }
        public ApiUser ApiUser { get; set; }
        public YoutubeChanell YoutubeChanell { get; set; }
    }
}
