using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dialog
{
    public class Message
    {
        public string MessageText { get; set; }
        public DateTime SentDate { get; set; }
        public bool Im { get; set; }
    }
}
