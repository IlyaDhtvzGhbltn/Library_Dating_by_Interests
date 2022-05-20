using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.WebApi.v1.Entities
{
    public class ApiUser : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserInternalId { get; set; }

        public string YoutubeUserId { get; set; }
        public string YoutubeUserName { get; set; }


        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<CustomerYoutubeChanell> Subscriptions { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
