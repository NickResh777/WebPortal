using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using DatingHeaven.Core;
using JetBrains.Annotations;
using Ninject;
using NLog;
using WebPortal.DataAccessLayer.FluentSyntax;
using WebPortal.DataAccessLayer.Infrastructure;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Repositories {
    public abstract class EfBaseRepository<T>: IRepository<T> where T: BaseEntity{
        protected Logger Log;

        private                 IEntityInfoResolver          _entityInfoResolver;
        private      readonly   IDbContext                   _dbContext;
        protected    readonly   DbSet<T>                     Set;

        protected EfBaseRepository(IDbContext dbContext){
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

        public IList<T> GetAll(params Expression<Func<T, object>>[] incProps)
        {
            var queryGetAll = from ent in Set
                              select ent;

            // include entity related properties
            IncludeEntityPropertiesInQuery(queryGetAll, incProps);

            try{
                return queryGetAll.ToList();
            }catch (Exception ex)
            {
                Log.Error(ex, "Failed to get entities");
                throw;
            }
        }


        public IList<T> GetWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] incProps)
        {
            IQueryable<T> query = Set.AsNoTracking();

            // include entity related properties if needed
            if (incProps != null){
                // append related properties
                IncludeEntityPropertiesInQuery(query, incProps);
            }
           
            // add the WHERE filter
            query = query.Where(predicate);

            try{
                return query.ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed");
                throw;
            }
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


        public virtual void DeleteById(object entityKey){
            if ((entityKey is int) && (typeof(BaseBusinessEntityWithId).IsAssignableFrom(typeof(T)))){
                var generator = new DeleteEntitySqlGenerator{
                       // init the TableName 
                       TableName = GetEntityInfoResolver().GetTableName<T>()
                };
                 
                // add condition, ---> WHERE [Id] = @id
                generator.WhereConditions.Add(new WhereCondition{
                     Column = "Id",
                     Operator = Comparison.Equals,
                     Value = entityKey
                });

                try{
                    int result = _dbContext.Database.ExecuteSqlCommand(
                                  sql: generator.GenerateSql(),
                                  parameters: generator.BuildParametersList()
                         );
                } catch (Exception ex){
                    Log.Error(ex, "Method <DeleteById> failed");
                    throw;
                }
            } 
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

        public FluentSyntaxForWhereConditions<T> Where(){
            return new FluentSyntaxForWhereConditions<T>(
                propertySelectionAnalyzer: GetPropertySelectionAnalyzer(),
                dbContext: _dbContext,
                entityInfoResolver: GetEntityInfoResolver()
            );
        }

        public IQueryable<T> Table {
            get{
                return Set;
            }
        }
  
        protected void IncludeEntityPropertiesInQuery(IQueryable<T> query,
                                                      params Expression<Func<T, object>>[] includedProperties){
                   var incList = includedProperties.ToList();
                   incList.ForEach((incExpression) => { query = query.Include(incExpression); });
        }

        protected IEntityInfoResolver GetEntityInfoResolver(){
            if (_entityInfoResolver == null){
                  // resolve EntityInfoResolver
                  _entityInfoResolver = GlobalInjectionContainer.Instance.Get<IEntityInfoResolver>();
            }

            return _entityInfoResolver;
        }

        protected IEntityPropertySelectionAnalyzer GetPropertySelectionAnalyzer(){
            // get the property selection analyzer
            return GlobalInjectionContainer.Instance.Get<IEntityPropertySelectionAnalyzer>();
        }
    }
}
