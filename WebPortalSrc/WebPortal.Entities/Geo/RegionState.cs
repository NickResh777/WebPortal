using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebPortal.Entities.Geo {
    [Table("RegionStates")]
    public class RegionState : BaseLookupEntity {

        public int CountryId{
            get; 
            set; 
        }

        public virtual Country Country{
            get; 
            set; 
        }

        public virtual IList<City> Cities{
            get; 
            set; 
        }
    }
}
