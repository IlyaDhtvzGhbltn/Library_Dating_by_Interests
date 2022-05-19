using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Infrastructure
{
    public class AppSettings
    {
        public const string BlobStorageKey = "AzureOptions:Storage:Key";
        public const string BlobStorageConnectionString = "AzureOptions:Storage:ConnectionString";
        public const string BlobStorageContainerName = "AzureOptions:Storage:ContainerName";
        public const string BlobStorageAccountName = "AzureOptions:Storage:AccountName";
    }
}
