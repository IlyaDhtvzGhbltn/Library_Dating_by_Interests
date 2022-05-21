using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.WebApi.v1.Entities
{
    public class YoutubeChanell
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string YoutubeId { get; set; }
        public string YoutubeTitle { get; set; }
        public string YoutubeDescription { get; set; }

        public virtual ICollection<Thumbnail> Thumbnails { get; set; }


        public virtual ICollection<User> LibraryUsers { get; set; }
    }
}
