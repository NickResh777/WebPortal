using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;


namespace WebPortal.Entities {
    public abstract class BaseBusinessEntity: BaseEntity {

        /// <summary>
        /// Date when this entity was created
        /// </summary>
        public DateTime CreatedOn{
            get; 
            set; 
        }


        [Timestamp]
        public byte[] RowVersion{
            get; 
            set; 
        }

    }
}
