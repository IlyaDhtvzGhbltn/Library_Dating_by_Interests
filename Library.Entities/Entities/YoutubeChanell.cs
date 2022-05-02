using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Entities
{
    public class YoutubeChanell
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Thumbnail> Thumbnails { get; set; }

        public virtual ICollection<CustomerYoutubeChanell> Customers { get; set; }
    }
}
