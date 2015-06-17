using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.Entities
{
    public class EntityTypesHelper
    {
        private EntityTypesHelper() {}

        private static readonly Type[] EntityTypes = {
            typeof(BaseBusinessEntityWithId),
            typeof(BaseLookupEntity),
            typeof(BaseBusinessEntity),
            typeof(BaseEntity)
        };

        public static bool IsBaseBusinessEntityWithId(Type type){
            bool result = false;
            Type currentType = type;

            while(currentType != null){
                if (currentType == typeof (BaseBusinessEntityWithId)){
                      result = true;
                      break;
                }
                currentType = currentType.BaseType;
            }
                                         
            return result;
        }

        public static bool IsEntityType(Type type){
            bool result = false;
            Type currentType = type;

            while(currentType != null){
                if (EntityTypes.Contains(currentType)){
                       result = true;
                       break;
                }

                currentType = currentType.BaseType;
            }

            return result;
        }
    }
}
