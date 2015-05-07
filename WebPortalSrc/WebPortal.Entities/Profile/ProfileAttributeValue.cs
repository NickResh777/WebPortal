using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebPortal.Entities.Profile {
    [Table("ProfileAttributeValues")]
    public class ProfileAttributeValue : BaseBusinessEntity {

        public int Id{
            get; 
            set; 
        }

        public int ProfileAttributeId{
            get; 
            set; 
        }

        public int TypeId{
            get; 
            set; 
        }


        public int? IntValue{
            get; 
            set; 
        }

        public string CharValue{
            get; 
            set; 
        }

        public string StringValue{
            get; 
            set; 
        }

        public string String100Value{
            get; 
            set; 
        }

        public byte[] BinaryValue{
            get; 
            set; 
        }

        [NotMapped]
        public ProfileAttributeValueType Type{
            get{
                return (ProfileAttributeValueType) TypeId;
            }
        }

        public virtual ProfileAttribute ProfileAttribute{
            get; 
            set; 
        }
    }
}
                                         