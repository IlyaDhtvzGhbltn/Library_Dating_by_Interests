namespace Library.Contracts.Azure
{

    public class AzureBlobStorageOptions
    {
        public string ConnectionString { get; set; }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string BlobUrl { get; set; }
    }
}
