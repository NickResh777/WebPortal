using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities.Profile;

namespace WebPortal.DataAccessLayer.Mapping {
      class ProfileAttributeMap: BaseMap<ProfileAttribute>{
        public ProfileAttributeMap(){
            HasKey(pMap => pMap.Id);

            Property(pMap => pMap.Name)
                .IsRequired()
                .HasMaxLength(200);

            HasMany(pMap => pMap.Values)
                .WithRequired(profileAttrValue => profileAttrValue.ProfileAttribute)
                .HasForeignKey(profileAttrValue => profileAttrValue.ProfileAttributeId);
        }
    }
}
