using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings
{
    public class ReactionRequest : TrustedRequest
    {
        public Reaction Reaction { get; set; }
    }
}
