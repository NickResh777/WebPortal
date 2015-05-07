using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer {
    public static class TypesHelper{
        private readonly static Type BaseEntityType;
        private readonly static Type BaseBusinessEntityType;
        private readonly static Type BaseBusinessEntityWithIdType;
        private static readonly Type BaseLookupEntityType;

        static TypesHelper(){
            BaseEntityType = typeof (BaseEntity);
            BaseBusinessEntityType = typeof (BaseBusinessEntity);
            BaseBusinessEntityWithIdType = typeof (BaseBusinessEntityWithId);
            BaseLookupEntityType = typeof (BaseLookupEntity);
        }


        public static bool IsBaseEntity<T>() where T : class{
            return IsBaseEntity(typeof (T));
        }

        public static bool IsBaseEntity(Type type){
            return BaseEntityType.IsAssignableFrom(type);
        }

        public static bool IsBaseBusinessEntity<T>() where T : class{
            return IsBaseBusinessEntity(typeof (T));
        }

        public static bool IsBaseBusinessEntity(Type type){
            return BaseBusinessEntityType.IsAssignableFrom(type);
        }

        public static bool IsBaseBusinessEntityWithId(Type type){
            // check if the 'T' type is a "child" to the 'BaseBusinessEntity' type
            return BaseBusinessEntityWithIdType.IsAssignableFrom(type);
        }

        public static bool IsBaseBusinessEntityWithId<T>() where T : class{
            return IsBaseBusinessEntityWithId(typeof (T));
        }

        public static bool IsBaseLookupEntity(Type type){
            return BaseLookupEntityType.IsAssignableFrom(type);
        }

        public static bool IsBaseLookupEntity<T>() where T : class{
            return IsBaseLookupEntity(typeof (T));
        }
    }
}
                 