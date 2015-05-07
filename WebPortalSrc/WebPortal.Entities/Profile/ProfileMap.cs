using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebPortal.Entities.Profile {
    [Table("ProfileMap")]
    public class ProfileMap: BaseBusinessEntityWithId{

        public bool AddressIsShown{
            get; 
            set; 
        }

        public bool ReligionIsShown{
            get; 
            set; 
        }

        public bool OrientationIsShown{
            get; 
            set; 
        }
    }
}
