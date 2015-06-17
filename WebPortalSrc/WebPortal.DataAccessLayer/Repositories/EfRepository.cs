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
    public class EfRepository<T> : IRepository<T> where T : BaseEntity{
        protected Logger Log;

        private          IEntityInfoResolver       _entityInfoResolver;
        private          IEntityOperationsProvider _entityOperationsProvider;
        private readonly IDbContext                _dbContext;
        private readonly DbSet<T>                  _dbSet;

        public EfRepository(IDbContext dbContext){
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            Log = LogManager.GetLogger(GetType().Name);
            Log.Info("Repository created!");
        }

        protected DbSet<T> Set{
            get{
                return _dbSet;
            }
        } 

        public virtual T GetById(object entityKey, params Expression<Func<T, object>>[] includedProps){
            object[] keys = (entityKey is object[])
                ? (object[]) entityKey
                : new[]{entityKey};

            try{
                T foundEntity = Set.Find(keys);

                if ((foundEntity != null) && (includedProps != null)){
                    // load included properties to entity
                    LoadEntityRelatedProperties(foundEntity, includedProps.ToList());
                }

                return foundEntity;
            } catch (Exception ex){
                Log.Error(ex, "Failed to get entity by its id: {0}", keys);
                throw;
            }
        }

        private void LoadEntityRelatedProperties(T foundEntity, List<Expression<Func<T, object>>> entProperties){
            entProperties.ForEach(
                entPropertySelector => {
                    // include all needed related properties
                    _dbContext.ObjectContext.LoadProperty(foundEntity, entPropertySelector);
                }
             );
        }

    
        public IList<T> GetAll(params Expression<Func<T, object>>[] incProps){
            try{
                var queryGetAll = from ent in Set
                                  select ent;

                if (incProps != null){
                    // include properties
                    IncludeEntityPropertiesInQuery(ref queryGetAll, incProps);
                }

                return queryGetAll.ToList();
            } catch (Exception ex){
                Log.Error(ex, "Failed to get entities");
                throw;
            }
        }


        public IList<T> GetWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] incProps){
            try{
                IQueryable<T> query = Set;
                // add the WHERE filter
                query = query.Where(predicate);

                // include entity related properties if needed
                if (incProps != null){
                    // append related properties
                    IncludeEntityPropertiesInQuery(ref query, incProps);
                }

              
                return query.ToList();
            } catch (Exception ex){
                Log.Error(ex, "Failed");
                throw;
            }
        }


        public void Refresh(T entity){
            if (entity == null){
                  throw new NullReferenceException("entity");
            }
            _dbContext.MarkAsChanged(entity);
        }

        public void Insert(T entity){
            if (entity == null){
                 throw new NullReferenceException("entity");
            }

            Set.Add(entity);
        }

        public void Delete(T entity){
            if (entity == null){
                  throw new NullReferenceException("entity");
            }

            _dbContext.MarkAsDeleted(entity);
        }


        public virtual void DeleteById(object entityKey){
            Type entityType = typeof (T);
            object[] entityKeys = (entityKey is object[])
                                        ? (object[]) entityKey
                                        : new[]{entityKey};

            try{
                var entityOperationsProvider = GetEntityOperationsProvider();
                entityOperationsProvider.DeleteEntityByKey(entityType, entityKeys, _dbContext);
            } catch (Exception ex){
                Log.Error(ex, "Failed to delete entity by Id");
                throw;
            }
        }

        private IEntityOperationsProvider GetEntityOperationsProvider()
        {
            if (_entityOperationsProvider == null){

                _entityOperationsProvider = GlobalInjectionContainer.Instance.Get<IEntityOperationsProvider>();
            }

            return _entityOperationsProvider;
        }


        public int GetCount(){
            try{
                return Set.Count();
            } catch (Exception ex){
                Log.Error(ex, "Failed to get count of entites in the table: {0}", typeof (T).Name);
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

        public IQueryable<T> Table{
            get{
                return Set.AsNoTracking();
            }
        }

        protected void IncludeEntityPropertiesInQuery(ref IQueryable<T> query,
                                                      params Expression<Func<T, object>>[] includedProperties){
            var incList = includedProperties.ToList();
            query = incList.Aggregate(query, (current, selector) => current.Include(selector));
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


        public int SaveChanges(){
            try{
                return _dbContext.SaveChanges();
            } catch (Exception ex){
                Log.Error(ex, "<SaveChanges> method failed");
                throw;
            }
        }
    }
}
