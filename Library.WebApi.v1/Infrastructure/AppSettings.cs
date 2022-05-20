﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.v1.Infrastructure
{
    public class AppSettings
    {
        public const string BlobStorageKey = "AzureOptions:Storage:Key";
        public const string BlobStorageConnectionString = "AzureOptions:Storage:ConnectionString";
        public const string BlobStorageAccountName = "AzureOptions:Storage:AccountName";
        public const string BlobURL = "AzureOptions:Storage:BlobUrl";
        public const string ConnectionString = "ConnectionStrings:LibraryUsers";
    }
}
