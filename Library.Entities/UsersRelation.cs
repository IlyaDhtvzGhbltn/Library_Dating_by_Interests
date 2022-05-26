using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Entities
{
    public class UsersRelation
    {
        [Key]
        [DatabaseGenerated( DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("ApiUser_Relation_Requester")]
        public ApiUser Requester { get; set; }

        [Required]
        public bool IsRequesterPositiveReaction { get; set; }

        [ForeignKey("ApiUser_Relation_Responser")]
        public ApiUser Responser { get; set; }

        [Required]
        public bool IsResponserPositiveReaction { get; set; }
    }
}
