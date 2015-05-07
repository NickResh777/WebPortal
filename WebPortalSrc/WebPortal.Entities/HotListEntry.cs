using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities.Members;

namespace WebPortal.Entities {
    public class HotListEntry : BaseBusinessEntity {
        public int MemberId{
            get; 
            set; 
        }

        public int TargetMemberId{
            get; 
            set; 
        }

        public bool ShouldNotify{
            get; 
            set; 
        }

        public string Comment{
            get; 
            set; 
        }

        public virtual Member CurrentMember{
            get; 
            set; 
        }

        public virtual Member TargetMember{
            get; 
            set; 
        }
    }
}
