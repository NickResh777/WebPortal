using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.Entities {
    public interface IModifiableEntity {
        /// <summary>
        /// 
        /// </summary>
        DateTime? ModifiedOn{
            get; 
            set; 
        }
    }
}
