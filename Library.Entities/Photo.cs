using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities
{
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhotoId { get; set; }


        [MaxLength(264)]
        [DataType(DataType.Url)]
        [Required]
        public Uri PhotoUrl { get; set; }


        [Required]
        public bool IsAvatar { get; set; }
    }
}
