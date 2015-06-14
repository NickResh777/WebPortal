using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Repositories.PerEntity {
    class HotListEntriesRepository : EfBaseRepository<HotListEntry>{
        public HotListEntriesRepository(IEntitySqlGeneratorsProvider sqlGeneratorsFactory, IEntityPropertySelectionAnalyzer propertySelectionAnalyzer, IDbContextProvider dbContextProvider, IEntityInfoResolver entityInfoResolver) : base(sqlGeneratorsFactory, propertySelectionAnalyzer, dbContextProvider, entityInfoResolver){}


        public override HotListEntry GetById(object entityKey){
            IQueryable<HotListEntry> query = GetByIdQuery(entityKey);

            try{
                return Queryable.FirstOrDefault(query);
            } catch (Exception ex){
                Log.Error(ex, "Failed to get HotListEntry entity by key");
                throw;
            }
        }


        private IQueryable<HotListEntry> GetByIdQuery(object key){
            dynamic entityKeys = new ExpandoObject();
            entityKeys.MemberId = (int)((object[])key)[0];
            entityKeys.TargetMemberId = (int)((object[])key)[1];

            var query = from hle in Set
                        where (hle.MemberId == entityKeys.MemberId) &&
                              (hle.TargetMemberId == entityKeys)
                        select hle;
            return query;
        } 


        public override HotListEntry GetByIdInclude(object key, 
                                                    params Expression<Func<HotListEntry, object>>[] includedProperties){
            IQueryable<HotListEntry> query = GetByIdQuery(key);

            // include properties if needed
            if (HasAnyIncludedProperty(includedProperties)){
                var inclusionList = includedProperties.ToList();
                inclusionList.ForEach((selector) => {
                    query = query.Include(selector);
                });
            }
          
            try{
                return Queryable.FirstOrDefault(query);
            } catch (Exception ex){
                Log.Error(ex, "Failed to get a <HotListEntry> entity");
                throw;
            }
        }



        public override IList<HotListEntry> GetWhereInclude(Expression<Func<HotListEntry, object>> propertySelector, object propertyValue, params Expression<Func<HotListEntry, object>>[] includeProperties) {
            throw new NotImplementedException();
        }
    }
}
