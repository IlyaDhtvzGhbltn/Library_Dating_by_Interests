using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.WebApi.v1.Entities
{
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhotoId { get; set; }


        [MaxLength(120)]
        [DataType(DataType.Url)]
        [Required]
        public Uri PhotoUrl { get; set; }


        [Required]
        public bool IsAvatar { get; set; }
    }
}
