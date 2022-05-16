using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dialog
{
    public class DialogPreview
    {
        public string DialogId { get; set; }
        public Interlocutor Interlocutor { get; set; }
        public string LastMessageCuttedText { get; set; }
        public DateTime LastMessageSentDate { get; set; }
        public int UnreadMessagesCount { get; set; }
    }
}
