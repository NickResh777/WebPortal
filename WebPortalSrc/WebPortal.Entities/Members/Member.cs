using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WebPortal.Entities.Members;

namespace WebPortal.Entities.Members {
    [Table("Members")]
    public class Member: BaseBusinessEntityWithId{
       
        public string NickName{
            get; 
            set; 
        }

        public string FirstName{
            get; 
            set; 
        }

        public string LastName{
            get; 
            set;
        }

        
        public string Gender{
            get; 
            set; 
        }


        [NotMapped]
        public Gender GenderValue{
            get{
                if (Gender == "F"){
                     // FEMALE 
                    return Members.Gender.Female;
                }  else if (Gender == "M"){
                    // MALE 
                    return Members.Gender.Male;
                }

                return Members.Gender.NotDefined;
            }
        }

        public string Email{
            get; 
            set; 
        }

        public DateTime? LastVisit{
            get; 
            set;
        }

        public virtual Profile.Profile Profile{
            get; 
            set; 
        }

        public virtual AuthInfo AuthInfo{
            get; 
            set; 
        }
    }
}
