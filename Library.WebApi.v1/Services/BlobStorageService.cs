using Azure.Storage;
using Azure.Storage.Blobs;
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

        public BlobStorageService(AzureBlobStorageOptions options)
        {
            _connectionString = options.ConnectionString;
            _containerName = options.ContainerName;
            _accountName = options.AccountKey;
            _accountKey = options.AccountKey;
        }

        public string SaveFile(Stream fileStream)
        {
            var service = new BlobServiceClient(_connectionString);
            BlobContainerClient container = service.GetBlobContainerClient(_containerName);
            container.CreateIfNotExists();

            BlobContainerClient client = new BlobContainerClient(
                container.Uri,
                new StorageSharedKeyCredential(_accountName, _accountKey)
              );

            string fileName = Guid.NewGuid().ToString();
            client.UploadBlob(fileName, fileStream);
            return fileName;
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
