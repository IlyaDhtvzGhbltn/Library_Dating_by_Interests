namespace Library.Contracts.Azure
{

    public class AzureBlobStorageOptions
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
    }
}
