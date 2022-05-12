using Library.Contracts.MobileAndLibraryAPI.DTO.Dating;
using Library.Contracts.MobileAndLibraryAPI.DTO.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DummyServices.DummyDto
{
    public class DummyDatingCriteria : DatingCriteriaBase
    {
        public DummyDatingCriteria(DatingCriteriaBase criteria)
        {
            this.Age = criteria.Age;
            this.Gender = criteria.Gender;
            this.Geo = criteria.Geo;
            this.MySubscriptions = criteria.MySubscriptions;
        }

        public override int GetHashCode()
        {
            int genderHash = (int)this.Gender.Gender;
            int geoHash = this.Geo.RadiusKm;
            geoHash += Geo.All == true ? 1 : 0;

            int ageHash = this.Age.MinAge;
            ageHash += this.Age.MaxAge;

            int summ = genderHash + geoHash + ageHash;
            return summ;
        }

        public override bool Equals(object obj)
        {
            DatingCriteriaBase criteria = obj as DatingCriteriaBase;
            int hashFirst = criteria.GetHashCode();
            int hashSecond = this.GetHashCode();
            if (hashFirst == hashSecond)
            {
                return true;
            }
            else return false;
        }
    }
}
