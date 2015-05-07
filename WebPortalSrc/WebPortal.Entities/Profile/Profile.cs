using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WebPortal.Entities.Geo;

namespace WebPortal.Entities.Profile {
    [Table("Profiles")]
    public class Profile : BaseBusinessEntity {

        public int MemberId{
            get; 
            set; 
        }


        public DateTime? DateOfBirth{
            get; 
            set; 
        }

        #region === GEO PROPERTIES ===

        public int? CountryId{
            get; 
            set; 
        }

        public int? RegionStateId{
            get; 
            set; 
        }

        public int? CityId{
            get; 
            set; 
        }

        public int? ReligionId{
            get; 
            set; 
        }

        public int? OrientationId{
            get; 
            set; 
        }

        public virtual ProfileAttributeValue Orientation{
            get; 
            set; 
        }

        public virtual ProfileAttributeValue Religion{
            get; 
            set; 
        }

        #endregion

        public virtual Members.Member Member{
            get; 
            set; 
        }

        public virtual City City{
            get; 
            set; 
        }

        public virtual Country Country{
            get; 
            set; 
        }

        public virtual RegionState RegionState{
            get; 
            set; 
        }

        public virtual ProfileMap ProfileMap{
            get; 
            set; 
        }
    }
}
