using System;
using System.Collections.Generic;
using System.Text;

namespace Library.WebApi.v1.Entities
{
    public class CustomerYoutubeChanell
    {
        public string UserId { get; set; }
        public ApiUser User { get; set; }
        public string YoutubeChanellId { get; set; }
        public YoutubeChanell YoutubeChanell { get; set; }
    }
}
