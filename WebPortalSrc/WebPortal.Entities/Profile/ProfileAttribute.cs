using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Ninject.Activation;

namespace WebPortal.Entities.Profile {
    [Table("ProfileAttributes")]
    public class ProfileAttribute : BaseLookupEntity {

        public ProfileAttribute(){
            HasMultipleValues = false;
        }

        public bool HasMultipleValues{
            get; 
            set; 
        }

        public virtual IList<ProfileAttributeValue> Values{
            get; 
            set; 
        } 
    }
}
