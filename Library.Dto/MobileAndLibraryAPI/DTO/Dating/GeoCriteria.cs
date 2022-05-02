using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dating
{
    public class GeoCriteria : IDatingCriteria
    {
        public int RadiusKm { get; set; }
        public bool All { get; set; }
    }
}
