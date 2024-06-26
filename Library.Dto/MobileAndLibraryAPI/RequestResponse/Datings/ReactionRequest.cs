﻿using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Datings
{
    public class ReactionResponse : IResponse
    {
        public ReactionResponse(int status)
        {
            Status = status;
        }
        public int Status { get; set; }
    }
}
