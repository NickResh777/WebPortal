using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Repositories {
    class EfSingleIdRepository<T> : EfBaseRepository<T> where T: BaseBusinessEntityWithId{

        public EfSingleIdRepository(IEntitySqlGeneratorsProvider sqlGeneratorsFactory,
            IEntityPropertySelectionAnalyzer propertySelectionAnalyzer,
            IEntityInfoResolver entityInfoResolver,
            IDbContext dbContext) :
                base(sqlGeneratorsFactory,
                    propertySelectionAnalyzer,
                    entityInfoResolver,
                    dbContext){
            
        }

        public override T GetById(object key, params Expression<Func<T, object>>[] includedProps){
            var entityKey = (int)key;
            var query = from ent in Set
                        where (ent.Id == entityKey)
                        select ent;

            // include properties if needed
            if (HasAnyIncludedProperty(includedProps)){
                var incList = includedProps.ToList();
                incList.ForEach((selector) =>{
                    // append new entity properties to the query
                    query = query.Include(selector);
                });
            }

            try{
                return query.FirstOrDefault();
            } catch (Exception ex){
                Log.Error(ex, "Failed to get");
                throw;
            }
        }

        public override T GetByIdInclude(object key, params Expression<Func<T, object>>[] includedProperties){
          
            try{
                return query
            }
        }

        public override IList<T> GetWhereInclude(Expression<Func<T, object>> propertySelector, object propertyValue, params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }
    }
}
