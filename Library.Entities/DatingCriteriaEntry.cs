using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities
{
    public class DatingCriteriaEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public virtual ApiUser User { get; set; }

        [Range(18, 80)]
        public int MinAge { get; set; }

        [Range(18, 80)]
        public int MaxAge { get; set; }

        [Range(2, 2000)]
        public int GeoRadiusKm { get; set; }

        public bool EnableGeoCriteria { get; set; }

        [Range(0, 2)]
        public int Gender { get; set; }
    }
}
