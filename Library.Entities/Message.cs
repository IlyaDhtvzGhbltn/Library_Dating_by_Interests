using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime SendingTime { get; set; }
        public string Text { get; set; }

        public virtual Dialog Dialog { get; set; }
        public virtual ApiUser Sender { get; set; }
    }
}
