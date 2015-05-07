using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;

namespace WebPortal.Entities {
    public abstract class BaseBusinessEntityWithId : BaseBusinessEntity{

        private int _entityId;

        protected BaseBusinessEntityWithId(){
            // ID is always null when being created
            Id = 0;
        }

        public int Id{
            get{
                return _entityId;
            }
            set{
                _entityId = value;
            }
        }
    }
}
