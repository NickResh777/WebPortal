using System;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.EntityAdapters {
    public class BaseBusinessEntityWidthIdAdapter<TEntity>: IEntityAdapter where TEntity : BaseEntity{
        private  BaseBusinessEntityWithId _entity;

        public BaseBusinessEntityWidthIdAdapter(BaseBusinessEntityWithId entity){
            _entity = entity;                 
        }

        public BaseBusinessEntityWidthIdAdapter(){
            
        } 

        public int Id{
            get{
                return _entity.Id;
            }
            set{
                _entity.Id = value;
            }
        }

        public bool IsHidden{
            get{
                return false;
            }
            set{
                
            }
        }

        public BaseEntity Entity{
            get{
                return _entity;
            }
            set{
                if (!(value is BaseBusinessEntityWithId)){
                         // we need just a derived type here
                         throw new ArgumentException("Only [BaseBusinessEntityWithId]");
                }
                _entity = (BaseBusinessEntityWithId)value;
            }
        }


    }
}
