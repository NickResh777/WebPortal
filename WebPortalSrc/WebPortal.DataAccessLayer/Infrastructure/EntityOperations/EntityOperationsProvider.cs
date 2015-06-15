using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.Entities;
using System.Data.Entity;
using WebPortal.Entities.Geo;


namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations {
    public class EntityOperationsProvider : IEntityOperationsProvider{
        private readonly IEntityInfoResolver                     _entityInfoResolver;
        private readonly IDbContext                              _dbContext;

        public EntityOperationsProvider( IEntityInfoResolver   entityInfoResolver,                       
                                                  IDbContext   dbContext){
            _entityInfoResolver = entityInfoResolver;
            _dbContext = dbContext;   // dbContext //
        }

        public virtual object GetPropertyValue<T>(object entityKey, string entityProperty) where T : BaseEntity{
            EnsureEntityKeyIsValid(entityKey);
            SelectEntitySqlGenerator generator = new SelectEntitySqlGenerator();

            // initialize the generator
            InitializeSelectSqlGenerator<T>(     sqlGenerator: generator,
                                                    entityKey: entityKey,
                                           selectedProperties: entityProperty);
            
            // build query
            var querySelectProperty = _dbContext.Database.SqlQuery(
                // TODO: Finish with EntityProviderAnalyzer 
                 elementType: _entityInfoResolver.GetPropertyType<T>(entityProperty),
                 sql: generator.GenerateSql(),
                 parameters: MakeParametersFromEntityKey(entityKey)
            );
         
            //return the value
            var enumerator = querySelectProperty.GetEnumerator();

            try{
                object result = null;

                if (enumerator.MoveNext()){
                    // get a value from enumerator
                    result = enumerator.Current;
                }
                return result;
            } catch (Exception ex){
                // todo: Log error here
                throw;
            }
        }

        

        private void InitializeSelectSqlGenerator<T>
            (SelectEntitySqlGenerator sqlGenerator,
                object entityKey,
                params string[] selectedProperties) where T: BaseEntity{
            if (entityKey is int){
                var whereIdCondition = new WhereCondition{
                    Column = "Id",
                    Operator = Comparison.Equals,
                    Value = entityKey
                };
                sqlGenerator.WhereConditions.Add(whereIdCondition);
            } else if (entityKey is object[]){
                var keys = ((object[]) entityKey).ToList();
                keys.ForEach(key =>{
                    var whereCondition = new WhereCondition{
                        Column = _entityInfoResolver.GetEntityKeyPropertyName<T>(keys.IndexOf(key)),
                        Operator = Comparison.Equals,
                        Value = key
                    };
                    sqlGenerator.WhereConditions.Add(whereCondition);
                });
            }
        }

     

        private object[] MakeParametersFromEntityKey(object entityKey){
            object[] result = null;
           
            if (entityKey is int){
                result = new object[]{
                   entityKey
                };  
            } else if (entityKey is object[]){
                // composite key
                result = (object[])entityKey;
            }

            return result ?? new object[0];
        }


        public bool SetPropertyValue<T>(object entityKey, string property, object value) where T: BaseBusinessEntity{
            EnsureEntityKeyIsValid(entityKey);

            var updateEntityGen = new UpdateEntitySqlGenerator();
            //updateEntityGen.Key = entityKey;
            updateEntityGen.Set(property, value);

            var result = (_dbContext.Database.ExecuteSqlCommand(
                              sql: updateEntityGen.GenerateSql(),
                              parameters: MakeParametersFromEntityKey(entityKey)) > 0      
                        );

            return result;
        }


        public object GetPropertyValue<T>(object entityKey, Expression<Func<T, object>> propertySelector) where T: BaseEntity{
            EnsureEntityKeyIsValid(entityKey);
            // ensure the entity property is selected
            EnsurePropertyIsExpression(propertySelector);

            // get the property name
            var propertyName = GetPropertyNameFromExpression(propertySelector);
            return GetPropertyValue<T>(entityKey, propertyName);
        }


        public bool SetPropertyValue<T>(object entityKey, Expression<Func<T, object>> propertySelector, object value) where T : BaseBusinessEntity {
            EnsurePropertyIsExpression(propertySelector);
            var propertyName = GetPropertyNameFromExpression(propertySelector);

            return SetPropertyValue<T>(entityKey, propertyName, value);
        }

        private string GetPropertyNameFromExpression<T>(Expression<Func<T, object>> exp){
            if (exp.Body is MemberExpression){
                return ((MemberExpression) exp.Body).Member.Name;
            } else if (exp.Body is UnaryExpression){
                var prop = ((UnaryExpression) exp.Body).Operand.ToString();
                if (prop.IndexOf('.') > 0){
                      prop = prop.Substring(prop.IndexOf('.') + 1);
                }
                return prop;
            } 

            throw new ArgumentException("Could not cast");
        }

        private void EnsurePropertyIsExpression<T>(Expression<Func<T, object>> expression){
            var expBody = expression.Body;
            if (!(expBody is MemberExpression) && !(expBody is UnaryExpression)) {
                // No property selected! We need the entity PROPERTY only
                throw new InvalidOperationException("You did not specify a property!");
            }

            if (expBody is UnaryExpression){
                if (expBody.NodeType != ExpressionType.Convert){
                    throw new InvalidOperationException("Must be only <Convert> type!");
                }
            } 
        }


        private void EnsureEntityKeyIsValid(object entityKey){
            if (entityKey == null){
                //throw
                throw new NullReferenceException("entityKey");
            }



            var result = (entityKey is object[]) ||
                         (entityKey is int) ||
                         (entityKey is string);
            if (!result){
                //
                throw new ArgumentException("Entity Key must be of type: [int, string, EntityKey]");
            }
        }
    }
}
