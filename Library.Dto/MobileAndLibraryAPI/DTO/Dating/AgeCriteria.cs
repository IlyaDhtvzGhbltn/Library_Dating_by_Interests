using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dating
{
    public class AgeCriteria
    {
        public AgeCriteria()
        {}

        public AgeCriteria(int minAge, int maxAge)
        {
            MinAge = minAge;
            MaxAge = maxAge;
        }

        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}
