using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities.Geo;

namespace WebPortal.DataAccessLayer.Mapping.Geo {
    public class RegionStateMap : BaseMap<RegionState>{
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
