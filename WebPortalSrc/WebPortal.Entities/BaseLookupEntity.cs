using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace WebPortal.Entities {
    public abstract class BaseLookupEntity: BaseEntity {

        public int Id{
            get; 
            set; 
        }

        /// <summary>
        /// Name of the entry in the dictionary list
        /// </summary>
        public string Name{
            get; 
            set; 
        }
    }
}
