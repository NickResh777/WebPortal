using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebPortal.Entities {
    [Table("AuthInfo")]
    public class AuthInfo : BaseEntity {
        public int MemberId{
            get; 
            set; 
        }

        public string PasswordHash{
            get; 
            set; 
        }

        public string PasswordSalt{
            get; 
            set; 
        }

        public string LoginProvider{
            get; 
            set; 
        }

       
    }
}
