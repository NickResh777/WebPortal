using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace WebPortal.Entities.Geo {
    [Table("Countries")]
    public class Country: BaseLookupEntity {

        public string Code{
            get; 
            set; 
        }

        public virtual IList<RegionState> RegionStates {
            get;
            set;
        }

        public virtual IList<City> Cities {
            get;
            set;
        } 
    }
}
