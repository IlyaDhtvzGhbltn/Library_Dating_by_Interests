using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings
{
    public class ReactionRequest : ApiRequest
    {
        public Reaction Reaction { get; set; }
    }
}
