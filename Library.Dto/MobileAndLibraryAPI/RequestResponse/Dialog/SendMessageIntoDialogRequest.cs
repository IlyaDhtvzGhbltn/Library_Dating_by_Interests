namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Dialog
{
    public class SendMessageIntoDialogRequest : TrustedRequest
    {
        public string MessageText { get; set; }
    }
}
