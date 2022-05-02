namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse
{
    public class TrustedRequest : IRequest
    {
        public string InternalToken { get; set; }
    }
}
