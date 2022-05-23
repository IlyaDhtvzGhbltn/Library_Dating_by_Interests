namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse.Dialog
{
    public class SendMessageIntoDialogRequest : ApiRequest
    {
        public string MessageText { get; set; }
    }
}
