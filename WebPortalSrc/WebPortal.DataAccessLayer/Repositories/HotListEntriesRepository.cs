using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Repositories {
    class HotListEntriesRepository : EfBaseRepository<HotListEntry>{
        public HotListEntriesRepository(IEntitySqlGeneratorsProvider sqlGeneratorsFactory, IEntityPropertySelectionAnalyzer propertySelectionAnalyzer, IDbContextProvider dbContextProvider, IEntityInfoResolver entityInfoResolver) : base(sqlGeneratorsFactory, propertySelectionAnalyzer, dbContextProvider, entityInfoResolver){}

        public override HotListEntry GetByIdInclude(object entityKey, 
                                                    params Expression<Func<HotListEntry, object>>[] includedProperties) {
            EnsureEntityKeyIsValid(entityKey);
            var keys = new[]{
                (int) ((object[]) entityKey)[0],
                (int) ((object[]) entityKey)[1] 
            };

            if (externalDbContext != null){
                   return Invoke_GetByIdInclude(
                                memberId: keys[0],
                                targetMemberId: keys[1],
                                dbContext: externalDbContext,
                                includes: includedProperties);
            } else{
                using (var dbContext = dbContextProvider.CreateContext()){
                    return Invoke_GetByIdInclude(
                                memberId: keys[0],
                                targetMemberId: keys[1],
                                dbContext: dbContext,
                                includes: includedProperties);
                }
            }
        }

        private HotListEntry Invoke_GetByIdInclude
            (int memberId,
                int targetMemberId,
                IDbContext dbContext,
                params Expression<Func<HotListEntry, object>>[] includes){

            try{
                var query = from hotListEntry in dbContext.Set<HotListEntry>().AsNoTracking()
                    where (hotListEntry.MemberId == memberId) && (hotListEntry.TargetMemberId == targetMemberId)
                    select hotListEntry;

                if (HasAnyIncludedProperty(includes)){
                    var includesList = includes.ToList();
                    includesList.ForEach(include =>{
                        query = query.Include(include);
                    });
                }

                return query.FirstOrDefault();
            } catch (Exception ex){
                throw;
            }
        }

        private void EnsureEntityKeyIsValid(object entityKey){
            if (entityKey == null){
                throw new NullReferenceException("entityKey");
            }

            if (!(entityKey is object[])){
                throw new ArgumentException("Entity key should be composite! <MemberId>, <TargetMemberId>");
            }

            var keys = (object[]) entityKey;

            if (keys.Length < 2){
                throw new ArgumentException("The composite key should contain two keys: <MemberId>, <TargetMemberId>");
            }

            if (!(keys[0] is int) || !(keys[1] is int)){
                throw new ArgumentException("Error!");
            }
        }

        public override IList<HotListEntry> GetWhereInclude(Expression<Func<HotListEntry, object>> propertySelector, object propertyValue, params Expression<Func<HotListEntry, object>>[] includeProperties) {
            throw new NotImplementedException();
        }
    }
}
