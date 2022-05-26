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

        [Required]
        public double Latitude { get; set; }
        
        [Required]
        public double Longitude { get; set; }

        [ForeignKey("ApiUser_DatingCriteria")]
        public virtual DatingCriteriaEntry DatingCriterias { get; set; }

        public virtual ICollection<Dialog> Dialogs { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<ApiUser_YoutubeChannel> ApiUsers_YoutubeChannels { get; set; }
    }
}
