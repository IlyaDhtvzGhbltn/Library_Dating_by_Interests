using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Contracts
{
    public class AppSettings
    {
        public const string ConnectionString = "ConnectionStrings:LibraryUsers";

        public class AzureBlob
        {
            public const string BlobStorageKey = "AzureOptions:Storage:Key";
            public const string BlobStorageConnectionString = "AzureOptions:Storage:ConnectionString";
            public const string BlobStorageAccountName = "AzureOptions:Storage:AccountName";
            public const string BlobURL = "AzureOptions:Storage:BlobUrl";
        }

        public class JWT
        {
            public const string JwtIssuer = "Jwt:Issuer";
            public const string JwtAudience = "Jwt:Audience";
        }

        public class Youtube
        {
            public const string ProfileInfoUrl = "Youtube:ProfileInfoUrl";
            public const string SubscriptionsUrl = "Youtube:SubscriptionsUrl";
            public const string SubscriptionsNextPageUrl = "Youtube:SubscriptionsNextPageUrl";
        }
    }
}
