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
        public HotListEntriesRepository(IDbContext dbContext) : 
            base(dbContext) {}

        public override HotListEntry GetById(object key, params Expression<Func<HotListEntry, object>>[] incProps){
            try{

                return null;
            } catch (Exception ex){
                Log.Error(ex, "Failed to get HotListEntry entity by key");
                throw;
            }
        }
    }
}
