using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations {
    public class EntityInfoResolver: IEntityInfoResolver{
        private static readonly Dictionary<Type, TableAttribute> TableNames = new Dictionary<Type, TableAttribute>(); 
        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> EntityProperties = new Dictionary<Type, Dictionary<string, PropertyInfo>>();  
        private static readonly Dictionary<Type, string> EntityKeyPropertyNames = new Dictionary<Type, string>(); 

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
            TableNames.Add( type, tableAttribute);
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
    }
}
