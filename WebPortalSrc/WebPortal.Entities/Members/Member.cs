using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WebPortal.Entities.Authentication;
using WebPortal.Entities.Members;

namespace WebPortal.Entities.Members {
    [Table("Members")]
    public class Member: BaseBusinessEntityWithId{

        public Member(){

            IsTrial = true;
            IsVip = false;
        }

        /// <summary>
        ///  Login name of the member
        /// </summary>
       public string NickName { get; set; }

       /// <summary>
       /// First name of the member if provided (can be NULL)
       /// </summary>
       public string FirstName { get; set; }

       /// <summary>
       /// Last name of the member if provided (can be NULL)
       /// </summary>
       public string LastName { get; set; }

       /// <summary>
       /// Gender of the member. A single-symbol value
       /// 'F' - female, 'M' - male
       /// </summary>
       public string Gender { get; set; }

        
        public bool IsTrial{
            get; 
            set; 
        }

        public bool IsVip{
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

        public virtual AppUser AppUser{
            get; 
            set; 
        }
    }
}
