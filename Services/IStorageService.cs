using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library.Services
{
    public interface IStorageService
    {
        string SaveFile(Stream fileStream, string containerName);
        bool DelteFile(string fileName, string containerName);
        bool FileExists(string fileName, string containerName);
        string GetUriByID(string fileId);
    }
}
