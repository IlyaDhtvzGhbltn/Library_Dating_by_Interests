namespace Library.Contracts.MobileAndLibraryAPI.RequestResponse
{
    public class TrustedRequest : IRequest
    {
        public string InternalBearerToken { get; set; }
    }
}
