using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using Library.DummyServices.DummyDto;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.DummyServices
{
    public class DummyDatingService : IDatingService
    {
        private Dictionary<DummyDatingCriteria, string[]> _datingCreiteriaToInternalIdMaping 
            = new Dictionary<DummyDatingCriteria, string[]>();

        private IDictionary<string, DatingProfile> _internalIdToProfileMaping 
            = new Dictionary<string, DatingProfile>();

        private Random _rnd = new Random();


        public async Task<Uri[]> EligibleProfiles(DatingCriteriaBase criteriaBase)
        {
            DummyDatingCriteria criteria = new DummyDatingCriteria(criteriaBase);
            string urlTemplate = "https://localhost:44383/api/v1/dating/user/{0}";
            Uri[] profilesUri;

            if (_datingCreiteriaToInternalIdMaping.ContainsKey(criteria))
            {
                string[] eligibleProfileIds = _datingCreiteriaToInternalIdMaping[criteria];
                profilesUri = new Uri[eligibleProfileIds.Length];
                for (int i = 0; i < profilesUri.Length; i++)
                {
                    profilesUri[i] = new Uri(string.Format(urlTemplate, eligibleProfileIds[i]));
                }
                return profilesUri;
            }
            else 
            {
                int eligibleProfilesCount = _rnd.Next(1, 3);
                DatingProfile[] eligibleProfiles = new DatingProfile[eligibleProfilesCount];
                string[] internalIds = new string[eligibleProfilesCount];
                profilesUri = new Uri[eligibleProfilesCount];

                for (int i = 0; i < eligibleProfilesCount; i++) 
                {
                    var profile = new DatingProfile();
                    profile.CommonInfo.About = "Bla bla";
                    profile.CommonInfo.Age = criteria.Age.MinAge;
                    profile.CommonInfo.Gender = criteria.Gender.Gender;
                    profile.CommonInfo.Name = "Name Surname";

                    string internalId = Guid.NewGuid().ToString();

                    internalIds[i] = internalId;
                    profilesUri[i] = new Uri(string.Format(urlTemplate, internalId));
                    _internalIdToProfileMaping[internalId] = profile;

                    eligibleProfiles[i] = profile;
                }
                _datingCreiteriaToInternalIdMaping[criteria] = internalIds;
                return profilesUri;
            }
        }

        public Task<DatingProfile> EligibleProfile(string internalId)
        {
            throw new NotImplementedException();
        }

        public Task ProfileReaction(string senderId, string profileId, Reaction reaction)
        {
            throw new NotImplementedException();
        }
    }
}
