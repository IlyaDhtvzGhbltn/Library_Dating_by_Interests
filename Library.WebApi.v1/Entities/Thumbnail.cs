using System;
using System.Collections.Generic;
using System.Text;

namespace Library.WebApi.v1.Entities
{
    public class Thumbnail
    {
        public Guid Id { get; set; }
        public string Default { get; set; }
        public string Medium { get; set; }
        public string High { get; set; }

        public virtual YoutubeChanell Chanell { get; set; }
    }
}
