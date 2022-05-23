using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    public class Dialog
    {
        public Guid Id { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
