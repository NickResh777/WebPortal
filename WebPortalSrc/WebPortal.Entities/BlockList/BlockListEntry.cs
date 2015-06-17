using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPortal.Entities.BlockList {
    [Table("BlockList")]
    public class BlockListEntry : BaseBusinessEntity{

        private BlockReason _blockReason;
        private BlockDuration _blockDuration;

        public BlockListEntry(){
            // block is active since start
            IsActive = true;
        }

        [Key]
        [Required]
        public int BlockEntryId{
            get; 
            set; 
        }

        [Required]
        public int MemberId{
            get; 
            set; 
        }


        [Required]
        public int BlockedMemberId{
            get; 
            set; 
        }

        [Required]
        public bool IsActive{
            get; 
            set; 
        }

        public BlockReason BlockReason{
            get{
                return _blockReason;
            }
            set{
                _blockReason = value;
            }
        }

        public BlockDuration BlockDuration{
            get{
                return _blockDuration;
            }
            set{
                _blockDuration = value;
            }
        }
    }
}
