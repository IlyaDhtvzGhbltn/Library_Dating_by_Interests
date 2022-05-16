using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts.MobileAndLibraryAPI.DTO.Dialog
{
    public class Dialog
    {
        public Interlocutor Interlocutor { get; set; }
        public string FriendsOnlineStatus { get; set; }
        public bool IsFriendOnline { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
