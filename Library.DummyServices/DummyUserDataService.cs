using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.Contracts.MobileAndLibraryAPI.DTO;
using Library.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Contracts.MobileAndLibraryAPI.RequestResponse;
using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using System;

namespace Library.DummyServices
{
    public class DummyUserDataService : IUserDataService
    {
        private Dictionary<Guid, UserProfile> _profileDB = new Dictionary<Guid, UserProfile>();

        public async Task DeleteProfile(Guid internalId)
        {
            _profileDB.Remove(internalId);
        }

        public async Task ChangeUserCommonInfo(Guid internalId, CommonInfo info)
        {
            _profileDB[internalId].CommonInfo.About = info.About;
            _profileDB[internalId].CommonInfo.Age = info.Age;
            _profileDB[internalId].CommonInfo.Gender = info.Gender;
            _profileDB[internalId].CommonInfo.Name = info.Name;
        }

        public async Task ChangeUserDatingCriteria(Guid internalId, DatingCriteria criteria)
        {
            _profileDB[internalId].DatingCriterias.Age = criteria.Age;
            _profileDB[internalId].DatingCriterias.Gender = criteria.Gender;
            _profileDB[internalId].DatingCriterias.Geo.All = criteria.Geo.All;
            _profileDB[internalId].DatingCriterias.Geo.RadiusKm = criteria.Geo.RadiusKm;
        }

        public async Task<UserProfile> GetProfileByInternalId(Guid internalId)
        {
            UserProfile profile;

            bool exist = _profileDB.TryGetValue(internalId, out profile);
            if (!exist) 
            {
                profile = new UserProfile();

                profile.CommonInfo.Name = "Ivan";
                profile.CommonInfo.Age = 26;
                profile.CommonInfo.Gender = Gender.Men;
                profile.CommonInfo.About = "Hi! My name is Ivan. Im from Moskow, metro Pushkin. Do you know it?";

                _profileDB[internalId] = profile;
            }

            return profile;
        }
    }
}
