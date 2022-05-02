using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dating
{
    public class GenderCriteria : IDatingCriteria
    {
        public Gender Gender { get; set; }
    }
}
