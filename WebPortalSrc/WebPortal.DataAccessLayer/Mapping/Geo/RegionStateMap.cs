using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using WebPortal.Entities.Geo;

namespace WebPortal.DataAccessLayer.Mapping.Geo {
    public class RegionStateMap : EntityTypeConfiguration<RegionState>{
        public RegionStateMap(){
            ToTable("RegionStates");

            HasKey(rs => rs.Id);

            HasRequired(regionState => regionState.Country)
                .WithMany(country => country.RegionStates)
                .HasForeignKey(regionState => regionState.CountryId).
                WillCascadeOnDelete(false);
         
        }
    }
}
