using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.EntityAdapters {
    public class EntityAdapter<TEntity> : IEntityAdapter where TEntity: BaseEntity{
        private BaseEntity _entity;

        /// <summary>
        /// Get the property name through the propertySelector
        /// </summary>  
        /// <param name="propertySelector"></param>
        /// <returns></returns>
        public EntityAdapter<TEntity> Property(Expression<Func<TEntity, object>> propertySelector){


            return this;
        }

        public bool Is(object propertyValue){


            return true;
        }



        public BaseEntity Entity {
            get{
                return _entity;
            }
            set{
                _entity = value;
            }
        }

        public void Property(Expression<Func<object, object>> propertySelector){
            throw new NotImplementedException();
        }
    }
}
