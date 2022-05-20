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
        private string _accountName;
        private string _accountKey;
        private string _blobURL;

        public BlobStorageService(AzureBlobStorageOptions options)
        {
            _connectionString = options.ConnectionString;
            _accountName = options.AccountName;
            _accountKey = options.AccountKey;
            _blobURL = options.BlobUrl;
        }

        public string SaveFile(Stream fileStream, string containerName)
        {
            var service = new BlobServiceClient(_connectionString);
            BlobContainerClient container = service.GetBlobContainerClient(containerName);
            container.CreateIfNotExists(PublicAccessType.BlobContainer);

            string fileName = Guid.NewGuid().ToString();
            BlobClient  blobClient = container.GetBlobClient(fileName);
            BlobHttpHeaders headers = new BlobHttpHeaders()
            {
                ContentType = "images"
            };
            blobClient.Upload(fileStream, headers);

            string url = string.Format(_blobURL, containerName, fileName);
            return url;
        }

        public bool DelteFile(string fileName, string containerName)
        {
            var service = new BlobServiceClient(_connectionString);
            BlobContainerClient container = service.GetBlobContainerClient(containerName);
            bool exist = FileExists(fileName, containerName);
            if (exist)
            {
                Response result = container.DeleteBlob(fileName);
                if (result.Status == 202)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
        }

        public bool FileExists(string fileName, string containerName)
        {
            var service = new BlobServiceClient(_connectionString);
            BlobContainerClient container = service.GetBlobContainerClient(containerName);
            BlobClient blob = container.GetBlobClient(fileName);
            bool exists = false;
            try
            {
                exists = blob.Exists();
            }
            catch (Exception)
            {
            }
            return exists;
        }

        public string GetUriByID(string id)
        {
            throw new NotImplementedException();
        }

    }
}
