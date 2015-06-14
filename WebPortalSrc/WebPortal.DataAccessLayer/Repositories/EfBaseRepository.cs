using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using JetBrains.Annotations;
using NLog;
using WebPortal.DataAccessLayer.FluentSyntax;
using WebPortal.DataAccessLayer.Infrastructure;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Repositories {
    public abstract class EfBaseRepository<T>: IRepository<T> where T: BaseEntity{
        protected Logger Log;

        private readonly IEntityInfoResolver              _entityInfoResolver;
        private readonly IEntitySqlGeneratorsProvider     _sqlGeneratorsProvider;
        private readonly IEntityPropertySelectionAnalyzer _propertySelectionAnalyzer;
        private      readonly   IDbContext                   _dbContext;
        protected    readonly   DbSet<T>                     Set;

        protected EfBaseRepository(IEntitySqlGeneratorsProvider sqlGeneratorsFactory, 
                                   IEntityPropertySelectionAnalyzer propertySelectionAnalyzer, 
                                   IEntityInfoResolver entityInfoResolver, 
                                   IDbContext dbContext){
            _sqlGeneratorsProvider = sqlGeneratorsFactory;
            _propertySelectionAnalyzer = propertySelectionAnalyzer;
            _entityInfoResolver = entityInfoResolver;
            _dbContext = dbContext;
            Set = _dbContext.Set<T>();
            Log = LogManager.GetLogger(GetType().Name);
        }

        public virtual T GetById(object entityKey, params Expression<Func<T, object>>[] includedProps ){
            object[] keys = (entityKey is object[])
                ? (object[]) entityKey
                : new[] { entityKey };

            try {
                  T foundEntity = Set.Find(keys);
                  return foundEntity;
            }catch (Exception ex){
                Log.Error(ex, "Failed to get entity by its id: {0}", keys);
                throw;
            }
        }
  
        public IList<T> GetAll(){
            try{
                var queryGetAll = from ent in Set
                                  select ent;
                return queryGetAll.ToList();
            } catch (Exception ex){
                Log.Error(ex, "Failed to get all entities from table");
                throw;
            }
        }

        #region === Abstract methods ==

           // Get a signle entity by id with included properties
           public abstract T GetByIdInclude(object entityKey, params Expression<Func<T, object>>[] includedProperties);

           public abstract IList<T> GetWhereInclude(
               Expression<Func<T, object>> propertySelector, 
               object propertyValue,
               params Expression<Func<T, object>>[] includeProperties);

        #endregion


        public IList<T> GetWhere(Expression<Func<T, object>> propertySelector, object propertyValue){
            // validate the current selector
            _propertySelectionAnalyzer.ValidateSelector<T>(propertySelector);

            // get the entity property name from the current selector
            string propertyName = _propertySelectionAnalyzer.GetPropertyName<T>(propertySelector);
 
            // call the internal method that invokes calls to SQL source
            return GetWhereInternal(propertyName, Comparison.Equals, propertyValue);
        }

        
                          
        public IList<T> GetWhere(Expression<Func<T, object>> propertySelector, 
                                     Comparison comparison, 
                                     object propertyValue){
            _propertySelectionAnalyzer.ValidateSelector(propertySelector);
            
            // get property name of entity using a selector expression
            string propertyName = _propertySelectionAnalyzer.GetPropertyName(propertySelector);

            return GetWhereInternal(propertyName, comparison, propertyValue);
        }

        

        private IList<T> GetWhereInternal(string propertyName, 
                                          Comparison comparison, 
                                          object propertyValue){
            IList<T> result = null;

            
                SelectEntitySqlGenerator sqlGenerator = _sqlGeneratorsProvider.CreateSelectGenerator();

                // initialize the query generator
                InitializeSqlGenerator(sqlGenerator, propertyName, comparison, propertyValue);

            using (var dbContext = dbContextProvider.CreateContext()){
                try{
                    var query = dbContext.Set<T>().SqlQuery(
                        sql: sqlGenerator.GenerateSql(),
                        parameters: _sqlGeneratorsProvider.BuildParametersList(sqlGenerator.WhereConditions));
                    result = query.ToList();
                } catch (Exception){

                    throw;
                }
            }


            return result;
        }

        private void InitializeSqlGenerator(SelectEntitySqlGenerator sqlGenerator,
                string propertyName,
                Comparison comparison,
                object propertyValue){
            sqlGenerator.SelectAllColumns = true;
            sqlGenerator.TableName = _entityInfoResolver.GetTableName<T>();

            var whereCondition = new WhereCondition{
                Column = propertyName,
                Operator = comparison,
                Value = propertyValue
            };

            sqlGenerator.WhereConditions.Add(whereCondition);
        }

        public IList<T> GetWhere(string property, object propertyValue){
            return GetWhereInternal(property, Comparison.Equals, propertyValue);
        }

        public void Refresh(T entity) {
            _dbContext.MarkAsChanged(entity);
            try{
                _dbContext.SaveChanges();
            } catch (Exception ex){
                Log.Error(ex, "Failed to refresh entity: {0}", entity);
                throw;
            }
        }

        public void Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            try{
                _dbContext.SaveChanges();
            } catch (Exception ex){
                Log.Error(ex, "Failed to insert new entity: {0}", entity);
                throw;
            }
        }

        public void Delete(T entity) {
            _dbContext.MarkAsDeleted(entity);
            try{
                _dbContext.SaveChanges();
            } catch (Exception ex){
                Log.Error(ex, "Failed to delete entity: type: {0}, id: {1}", 
                               typeof(T).Name, 
                               entity.Keys);
                throw;
            }
        }


        public void DeleteById(object entityKey){
           //var generator =  _sqlGeneratorsFactory.CreateDeleteGenerator<T>();
           //InitializeGenerator(generator);
        }


        public int GetCount(){
            try{
                return Set.Count();
            } catch (Exception ex){
                Log.Error(ex, "Failed to get count of entites in the table: {0}", typeof(T).Name);
                throw;
            }
        }

        public int GetCountWhere(Expression<Func<T, bool>> predicate){
            try{
                return Set.Where(predicate).Count(); /* get count of matched entities after applied predicate */
            } catch (Exception ex){
                Log.Error(ex, "Failed to get count for predicate: {0}", predicate.Body);
                throw;
            } 
        }


        public IList<T> GetWhere(string property, Comparison comparison, object propertyValue){
            return GetWhereInternal(property, comparison, propertyValue);
        }


        public FluentSyntaxForWhereConditions<T> Where() {
            return new FluentSyntaxForWhereConditions<T>(
                              propertySelectionAnalyzer: _propertySelectionAnalyzer,
                              dbContext: dbContextProvider.CreateContext(),
                              sqlGeneratorsFactory: _sqlGeneratorsProvider,
                              entityInfoResolver: _entityInfoResolver
            );
        }

        public IQueryable<T> Table {
            get{
                return Set;
            }
        }


        protected bool HasAnyIncludedProperty(params Expression<Func<T, object>>[] includedProperties){
            // check if 
            return (includedProperties != null) && (includedProperties.Length > 0);
        }


    
    }
}
