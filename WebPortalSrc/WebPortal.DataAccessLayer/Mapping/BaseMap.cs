using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Mapping {
    public abstract class BaseMap<T>: EntityTypeConfiguration<T> where T: class{
       

        protected BaseMap(){
            
        }
    }
}
