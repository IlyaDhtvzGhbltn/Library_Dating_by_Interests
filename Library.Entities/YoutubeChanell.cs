using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities
{
    public class YoutubeChanell
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string YoutubeId { get; set; }
        public string YoutubeTitle { get; set; }
        public string YoutubeDescription { get; set; }

        public virtual Photo Avatar { get; set; }
        public virtual ICollection<ApiUser> LibraryUsers { get; set; }
    }
}
