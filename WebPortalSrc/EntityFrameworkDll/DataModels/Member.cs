using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EntityFrameworkDll.DataModels {
    public class Member{
        private List<Photo> _photos; 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{
            get; 
            set;
        }

        [Required]
        public string Login{
            get;
            set;
        }

        [Required]
        public string FirstName{
            get; 
            set;
        }

        [Required]
        public string LastName{
            get; 
            set;
        }

        public ICollection<Photo> Photos{
            get{
                return _photos ?? (_photos = new List<Photo>());
            } 
        }
    }
}
