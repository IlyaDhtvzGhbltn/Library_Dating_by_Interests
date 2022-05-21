using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.WebApi.v1.Entities
{
    public class User : IdentityUser
    {
        public Guid InternalId { get; set; }
        public string YoutubeUserId { get; set; }
        public string YoutubeUserName { get; set; }


        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<YoutubeChanell> Subscriptions { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
