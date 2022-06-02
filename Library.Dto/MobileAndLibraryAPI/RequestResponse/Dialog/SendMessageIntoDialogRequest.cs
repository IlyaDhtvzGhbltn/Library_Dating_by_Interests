using System;

namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Dialog
{
    public class SendMessageIntoDialogRequest : ApiRequest
    {
        public string MessageText { get; set; }
        public DateTime ClientTime { get; set; }
    }
}
