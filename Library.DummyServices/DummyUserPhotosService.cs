using Library.Services;
using System;

namespace Library.DummyServices
{
    public class DummyUserPhotosService : IUserPhotosService
    {
        public bool SetPhotoAsAvatar(string userId, string photoId)
        {
            return true;
        }

        public string SaveUserPhotoUrlDB(string userId, Uri photoUri) 
        {
            string photoId = Guid.NewGuid().ToString();
            return photoId;
        }

        public bool DeletePhotoFromDB(string photoId) 
        {
            return true;
        }
    }
}
