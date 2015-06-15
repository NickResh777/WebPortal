using System;
using System.Linq;
using System.Linq.Expressions;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Repositories {
    class EfSingleIdRepository<T> : EfBaseRepository<T> where T: BaseBusinessEntityWithId{
        public EfSingleIdRepository(IDbContext dbContext) : 
            base(dbContext) {}

        public override T GetById(object key, params Expression<Func<T, object>>[] includedProps){
            var entityKey = (int)key;
            var query = from ent in Set
                        where (ent.Id == entityKey)
                        select ent;

            // include properties if needed
            if (includedProps != null){
                  IncludeEntityPropertiesInQuery(query, includedProps);
            }

            try{
                return query.FirstOrDefault();
            } catch (Exception ex){
                Log.Error(ex, "Failed to get");
                throw;
            }
        }
    }
}
