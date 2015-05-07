using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities.Geo;

namespace WebPortal.DataAccessLayer.Mapping.Geo {
    public class CountryMap: BaseMap<Country>{
        public CountryMap(){
            ToTable("Countries");

            HasKey(c => c.Id);

            Property(country => country.Code).
                IsRequired().
                HasMaxLength(10);

            //HasMany(country => country.RegionStates).
            //    WithRequired(rs => rs.Country).
            //    HasForeignKey(rs => rs.CountryId);

            //HasMany(country => country.Cities).
            //   WithRequired(city => city.Country).
            //   HasForeignKey(city => city.CountryId);
        }
    }
}
