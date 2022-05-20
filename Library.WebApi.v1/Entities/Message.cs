using System;
using System.Collections.Generic;
using System.Text;

namespace Library.WebApi.v1.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public DateTime SendingTime { get; set; }
        public string Text { get; set; }

        public virtual Dialog Dialog { get; set; }
        public virtual YoutubeChanell Sender { get; set; }
    }
}
