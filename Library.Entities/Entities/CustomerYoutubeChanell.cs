using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    public class CustomerYoutubeChanell
    {
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string YoutubeChanellId { get; set; }
        public YoutubeChanell YoutubeChanell { get; set; }
    }
}
