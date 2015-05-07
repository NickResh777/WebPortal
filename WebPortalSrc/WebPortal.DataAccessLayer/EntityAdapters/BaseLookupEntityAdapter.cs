using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.EntityAdapters {
    public class BaseLookupEntityAdapter: IEntityAdapter{
        private BaseLookupEntity _entityLookup;


        public BaseLookupEntityAdapter(BaseLookupEntity entityLookup){
            _entityLookup = entityLookup;
        }

        public BaseLookupEntityAdapter(){
            
        }

        public BaseEntity Entity {
            get{
                return _entityLookup;
            }
            set {
                if (!(value is BaseLookupEntity)){
                    throw new Exception("Must be a <BaseLookupEntity>!");
                }
                _entityLookup = (BaseLookupEntity)value;
            }
        }

        public int Id{
            get{
                return _entityLookup.Id;
            }
            set{
                _entityLookup.Id = value;
            }
        }
    }
}
