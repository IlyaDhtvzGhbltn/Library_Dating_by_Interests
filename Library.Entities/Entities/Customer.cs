using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Entities
{
    public class Customer
    {
        [Key]
        public string YoutubeUserId { get; set; }
        public string YoutubeUserName { get; set; }
        public virtual ICollection<Dialog> Dialogs { get; set; }

        public virtual ICollection<CustomerYoutubeChanell> Subscriptions { get; set; }
    }
}
