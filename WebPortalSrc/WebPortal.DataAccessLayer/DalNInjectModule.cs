using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.DataAccessLayer.Repositories;
using Ninject.Modules;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer {
    public class DalNInjectModule: NinjectModule {
        public override void Load(){
            // always create a new repository
            Bind<IRepository<HotListEntry>>().To<HotListEntriesRepository>();
            Bind(typeof (IRepository<>)).To(typeof(EfSingleIdRepository<>));
            Bind<IEntitySqlGeneratorsProvider>().To<EntitySqlGeneratorsProvider>();
            Bind<IDbContextProvider>().To<DbContextProvider>();
            Bind<IEntityPropertySelectionAnalyzer>().To<EntityPropertySelectionAnalyzer>();
            Bind<IEntityInfoResolver>().To<EntityInfoResolver>();
        }
    }
}
