using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities
{
    public class ApiUser : IdentityUser
    {
        public string YoutubeUserId { get; set; }

        public string About { get; set; }
        public int? Age { get; set; }
        [Required]
        public int Gender { get; set; }

        [ForeignKey("ApiUser_DatingCriteria")]
        public virtual DatingCriteria DatingCriterias { get; set; }

        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<YoutubeChanell> Subscriptions { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
