using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Library.Contracts.Azure;
using Library.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library.WebApi.v1.Services
{
    public class BlobStorageService : IStorageService
    {
        private string _connectionString;
        private string _containerName;
        private string _accountName;
        private string _accountKey;
        private string _blobURL;

        public BlobStorageService(AzureBlobStorageOptions options)
        {
            _connectionString = options.ConnectionString;
            _containerName = options.ContainerName;
            _accountName = options.AccountName;
            _accountKey = options.AccountKey;
            _blobURL = options.BlobUrl;
        }

        public string SaveFile(Stream fileStream)
        {
            var service = new BlobServiceClient(_connectionString);
            BlobContainerClient container = service.GetBlobContainerClient(_containerName);
            container.CreateIfNotExists();

            string fileName = Guid.NewGuid().ToString();
            BlobClient  blobClient = container.GetBlobClient(fileName);
            BlobHttpHeaders headers = new BlobHttpHeaders()
            {
                ContentType = "images"
            };
            blobClient.Upload(fileStream, headers);

            string url = _blobURL + fileName;
            return url;
        }

        public void DelteFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public bool FileExists(string path)
        {
            throw new NotImplementedException();
        }

        public string GetUriByID(string id)
        {
            throw new NotImplementedException();
        }

    }
}
