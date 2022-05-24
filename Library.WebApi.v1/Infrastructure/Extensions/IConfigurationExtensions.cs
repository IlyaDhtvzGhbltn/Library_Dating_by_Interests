using Library.Contracts;
using Library.Contracts.Azure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Infrastructure.Extensions
{
    public static class IConfigurationExtensions
    {
        public static AzureBlobStorageOptions GetAzureOptions(this IConfiguration config)
        {
            string blobKey = config[AppSettings.AzureBlob.BlobStorageKey];
            string connectionString = config[AppSettings.AzureBlob.BlobStorageConnectionString];
            string accountName = config[AppSettings.AzureBlob.BlobStorageAccountName];
            string blobURL = config[AppSettings.AzureBlob.BlobURL];

            var options = new AzureBlobStorageOptions()
            {
                AccountKey = blobKey,
                ConnectionString = connectionString,
                AccountName = accountName,
                BlobUrl = blobURL
            };

            return options;
        }
    }
}
