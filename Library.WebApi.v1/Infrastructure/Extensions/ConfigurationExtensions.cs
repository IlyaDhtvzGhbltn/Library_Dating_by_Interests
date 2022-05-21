using Library.Contracts.Azure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static AzureBlobStorageOptions GetAzureOptions(this IConfiguration config)
        {
            string blobKey = config.GetSection(AppSettings.BlobStorageKey).Value;
            string connectionString = config.GetSection(AppSettings.BlobStorageConnectionString).Value;
            string accountName = config.GetSection(AppSettings.BlobStorageAccountName).Value;
            string blobURL = config.GetSection(AppSettings.BlobURL).Value;

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
