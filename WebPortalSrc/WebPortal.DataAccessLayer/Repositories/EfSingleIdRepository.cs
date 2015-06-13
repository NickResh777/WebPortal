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
        public EfSingleIdRepository
            (IEntitySqlGeneratorsProvider sqlGeneratorsFactory,
                IEntityPropertySelectionAnalyzer propertySelectionAnalyzer,
                IDbContextProvider dbContextProvider,
                IEntityInfoResolver entityInfoResolver) :
                    base(sqlGeneratorsFactory,
                        propertySelectionAnalyzer,
                        dbContextProvider,
                        entityInfoResolver){
            
        }

        public override T GetByIdInclude(object entityKey, 
                                         params Expression<Func<T, object>>[] includedProperties) {
            if (externalDbContext != null){
                return Invoke_GetByIdInclude((int) entityKey, externalDbContext, includedProperties);
            } else{
                using (var dbContext = dbContextProvider.CreateContext()){
                    return Invoke_GetByIdInclude((int) entityKey, dbContext, includedProperties);
                }
            }
        }

        private T Invoke_GetByIdInclude(int entityId, 
            IDbContext dbContext,
            params Expression<Func<T, object>>[] includedProperties){

            T result = default (T);
            try {
                    var query = from ent in dbContext.Set<T>().AsNoTracking()
                                where (ent.Id == entityId)
                                select ent;

                    if (HasAnyIncludedProperty(includedProperties)){
                        query = includedProperties.Aggregate(query, 
                            (current, propertySelector) => current.Include(propertySelector));
                    }

            
               result =  query.FirstOrDefault();
            } catch (Exception ex){

                throw;
            }

            return result;
        }

        public override IList<T> GetWhereInclude(
            Expression<Func<T, object>> propertySelector, 
            object propertyValue, 
            params Expression<Func<T, object>>[] includeProperties) {
            if (externalDbContext != null){
                
            } else{
                
            }

            return null;
        }

       
    }
}
