using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities.Profile;

namespace WebPortal.DataAccessLayer.Mapping {
    public class ProfileMap : BaseMap<Profile>{
        public ProfileMap(){
            ToTable("Profiles");
            HasKey(p => p.MemberId);

            HasRequired(p => p.Member).
                WithRequiredDependent();

            Property(p => p.CityId);
            Property(p => p.RegionStateId);
            Property(p => p.CountryId);

            // Foreign key 'CityId' in the 'Profiles' table
            HasOptional(p => p.City).
                WithMany().
                HasForeignKey(p => p.CityId);

            // Foreign key 'RegionStateId' in the 'Profiles' table
            HasOptional(p => p.RegionState).
                WithMany().
                HasForeignKey(p => p.RegionStateId);

            // Foreign key 'CountryId' in the 'Profiles' table
            HasOptional(p => p.Country).
                WithMany().
                HasForeignKey(p => p.CountryId);

            // 'Orientation'
            HasOptional(p => p.Orientation)
                .WithMany()
                .HasForeignKey(p => p.OrientationId);

            // Religion
            HasOptional(p => p.Religion)
                .WithMany()
                .HasForeignKey(p => p.ReligionId);
        }
    }
}
