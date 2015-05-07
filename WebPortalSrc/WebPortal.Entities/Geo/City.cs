using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebPortal.Entities.Geo {
    [Table("Cities")]
    public class City: BaseLookupEntity {

        public int CountryId{
            get; 
            set; 
        }

        public int RegionStateId{
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
    }
}
