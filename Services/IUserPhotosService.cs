using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services
{
    public interface IUserPhotosService
    {
        bool SetPhotoAsAvatar(string userId, string photoId);
        string SaveUserPhotoUrlDB(string userId, Uri photoUri);
        bool DeletePhotoFromDB(string photoId);
    }
}
