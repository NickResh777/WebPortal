using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EntityFrameworkDll.DataModels {
    public class Photo {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{
            get; 
            set; 
        }

       
        public int MemberId{
            get; 
            set; 
        }

        public string Url{
            get; 
            set; 
        }

        public int Width{
            get; 
            set; 
        }

        public int Height{
            get; 
            set; 
        }

        public Member Member{
            get; 
            set; 
        }
    }
}
