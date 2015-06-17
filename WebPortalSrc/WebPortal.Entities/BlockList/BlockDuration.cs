using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace WebPortal.Entities.BlockList {
    [Table("BlockingDurations")]
    public class BlockDuration: BaseLookupEntity {

        public BlockDuration(){
            // 
            IsLifetime = false;
        }

        [Required]
        public int Days{
            get; 
            set; 
        }

        [Required]
        public bool IsLifetime{
            get; 
            set; 
        }
    }
}
