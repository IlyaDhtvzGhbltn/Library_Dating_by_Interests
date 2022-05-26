using System;
using System.Collections.Generic;

namespace Library.Entities
{
    public class Dialog
    {
        public Guid Id { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
