using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EntityFrameworkDll.DataModels {

    public class BaseEntity {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{
            get; 
            set;
        }


        public DateTime CreatedOn{
            get; 
            set; 
        }

        public DateTime ModifiedOn{
            get; 
            set; 
        }
    }
}
