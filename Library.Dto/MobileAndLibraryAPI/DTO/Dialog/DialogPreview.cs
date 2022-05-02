using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dialog
{
    public class DialogPreview
    {
        public Sender Sender { get; set; }
        public string LastMessageCuttedText { get; set; }
        public DateTime LastMessageSentDate { get; set; }
        public int UnreadMessagesCount { get; set; }
    }
}
