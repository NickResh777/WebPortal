using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using WebPortal.Entities.Geo;

namespace WebPortal.DataAccessLayer.Mapping.Geo {
    public class CityMap : EntityTypeConfiguration<City>{
        public CityMap(){
            ToTable("Cities");
            HasKey(c => c.Id);

            Property(city => city.Name).IsRequired().HasMaxLength(200);

            // foreign key to the 'RegionStates' table
            HasRequired(city => city.RegionState)
                .WithMany(regionState => regionState.Cities)
                .HasForeignKey(city => city.RegionStateId)
                .WillCascadeOnDelete(true);

             //foreign key to the 'Countries' table
            HasRequired(city => city.Country)
                .WithMany(country => country.Cities)
                .HasForeignKey(city => city.CountryId)
                .WillCascadeOnDelete(true);
        }
    }
}
