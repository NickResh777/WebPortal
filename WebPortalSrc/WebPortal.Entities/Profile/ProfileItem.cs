using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebPortal.Entities.Profile {
    [Table("ProfileItems")]
    public class ProfileItem : BaseBusinessEntity{
        private int _keyProfileId;
        private int _keyProfileAttributeId;

        [Required]
        [Key]
        [Column(Order = 0, TypeName = "integer")]
        public int ProfileId{
            get{
                return _keyProfileId;
            }
            set{
                _keyProfileId = value;
                OnEntityKeySet(_keyProfileId);
            }
        }

        [Required]
        [Key]
        [Column(Order = 1, TypeName = "integer")]
        public int ProfileAttributeId{
            get{
                return _keyProfileAttributeId;
            }
            set{
                _keyProfileAttributeId = value;
                OnEntityKeySet(_keyProfileAttributeId);
            }
        }

        [Required]
        [ForeignKey("AttributeValue")]
        public int AttributeValueId{
            get; 
            set; 
        }

        public ProfileAttributeValue AttributeValue{
            get; 
            set; 
        }
    }
}
