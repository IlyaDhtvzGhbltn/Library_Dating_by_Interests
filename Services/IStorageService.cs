using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library.Services
{
    public interface IStorageService
    {
        string SaveFile(Stream fileStream);
        void DelteFile(string fileName);
        bool FileExists(string path);
        string GetUriByID(string fileId);
    }
}
