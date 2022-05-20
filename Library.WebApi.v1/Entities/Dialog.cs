using System;
using System.Collections.Generic;
using System.Text;

namespace Library.WebApi.v1.Entities
{
    public class Dialog
    {
        public Guid Id { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
