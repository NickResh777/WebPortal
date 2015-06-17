using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Caching;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations {
    public class EntityInfoResolver: IEntityInfoResolver{
        private static readonly Dictionary<Type, TableAttribute> TableNames = new Dictionary<Type, TableAttribute>(); 
        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> EntityProperties = new Dictionary<Type, Dictionary<string, PropertyInfo>>();  
        private static readonly Dictionary<Type, string> EntityKeyPropertyNames = new Dictionary<Type, string>();


        static EntityInfoResolver(){
             ResolveTableNames();
        }

        private static void ResolveTableNames(){
            var entityTypesList = (from type in Assembly.GetAssembly(typeof (BaseEntity)).GetTypes()
                                   where EntityTypesHelper.IsEntityType(type) && !type.IsAbstract
                                   select type).ToList();

            entityTypesList.ForEach(et => {
                TableAttribute tableAttr = null;
                if (!TryGetTableAttribute(et, out tableAttr)){
                     // [TableAttribute] was not found for the current entity type
                     throw new ArgumentException("<TableAttribute> was not found for type: " + et.Name);
                }

                TableNames.Add(et, tableAttr);
            });
        }

        private static bool TryGetTableAttribute(Type entityType, out TableAttribute tableAttr){
            var attributes = entityType.GetCustomAttributes(attributeType: typeof(TableAttribute),
                                                            inherit: false);

            if (attributes.Length < 1) {
                // no TableAttribute was found in the current entity type
                tableAttr = null;
                return false;
            }

            tableAttr = (TableAttribute)attributes.First();
            return true;
        }


        public string GetTableName<T>() where T : class {
              EnsureTableAttributeExists<T>();
              return GetTableNameInternal<T>();
        }

        private void EnsureTableAttributeExists<T>() where T: class {
            if (!HasTableName<T>()){
                lock (TableNames){
                    if (!HasTableName<T>()){
                        // resolve table name
                        SolveTableName<T>();
                    }
                }
            }
        }

        private static string GetTableNameInternal<T>() where T : class {
            return TableNames[typeof (T)].Name;
        }

        private void SolveTableName<T>(){
            var type = typeof (T);
            var attributes = type.GetCustomAttributes(attributeType: typeof(TableAttribute),
                                                            inherit: false);

            if (attributes.Length < 1){
                // no TableAttribute was found in the current entity type
                throw new Exception("<TableAttribute> is not defined for type: " + type);
            }

            var tableAttribute = (TableAttribute)attributes.FirstOrDefault();
            TableNames.Add(type, tableAttribute);
        }

        private bool HasTableName<TEntity>(){

            return TableNames.ContainsKey(typeof (TEntity));
        }



        public string GetTableSchema<T>() where T : class {
            throw new NotImplementedException();
        }


        public Type GetPropertyType<T>(string property) where T : class {
            if (!HasEntityProperties<T>()){
                lock (EntityProperties){
                    if (!HasEntityProperties<T>()){
                            var type = typeof (T);
                            var properties = type.GetProperties().ToList();
                            var propertiesMap = new Dictionary<string, PropertyInfo>();
                            properties.ForEach(prop => propertiesMap.Add(prop.Name, prop));
                            EntityProperties.Add(type, propertiesMap);
                    }
                }
            }

            return EntityProperties[typeof (T)][property].PropertyType;
        }

        private static bool HasEntityProperties<T>(){
            return EntityProperties.ContainsKey(typeof (T));
        }


        public string GetEntityKeyProperty<T>() where T : class {
            if (!EntityKeyPropertyNames.ContainsKey(typeof (T))){
                lock (EntityKeyPropertyNames){
                    if (!EntityKeyPropertyNames.ContainsKey(typeof (T))){
                        var type = typeof (T);
                        var properties = type.GetProperties();
                        var keyPropertyName = string.Empty;
                        bool found = false;
                            foreach(var p in properties){
                            
                                var keyAttributes = p.GetCustomAttributes(typeof (KeyAttribute), false);
                                if ((keyAttributes.Length <= 0)) continue;
                                keyPropertyName = p.Name;
                                found = true;
                        }

                        if (!found){
                            // 
                            throw new InvalidOperationException("No Key property found!");
                        } else{
                           EntityKeyPropertyNames.Add(type, keyPropertyName);   
                        }
                    }
                }
            }

            return EntityKeyPropertyNames[typeof (T)];
        }


        public string[] GetOrderedKeyPropertyNames<T>() where T : BaseEntity {
            throw new NotImplementedException();
        }

        public string GetEntityKeyPropertyName<T>(int keyPropertyOrder) where T : BaseEntity {
            throw new NotImplementedException();
        }


        public string GetTableName(Type entityType){
            // get name from cached collection of [TableAttribute]
            if (entityType == null){
                  throw new NullReferenceException("entityType");
            }

            if (!TableNames.ContainsKey(entityType)){
                  throw new ArgumentException("Provided type is not entity type!"); 
            }

            return TableNames[entityType].Name;
        }
    }
}
