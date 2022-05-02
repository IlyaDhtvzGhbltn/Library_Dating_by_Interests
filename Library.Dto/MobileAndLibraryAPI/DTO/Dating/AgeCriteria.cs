using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dating
{
    public class AgeCriteria : IDatingCriteria
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}
