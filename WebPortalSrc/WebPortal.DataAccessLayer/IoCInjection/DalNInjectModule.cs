using Ninject.Modules;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.DataAccessLayer.Repositories;
using WebPortal.DataAccessLayer.Repositories.PerEntity;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.IoCInjection {
    public class DalNInjectModule: NinjectModule {
        public override void Load(){
            // always create a new repository
            Bind<IDbContext>().To<WebPortalDbContext>();
            Bind(typeof(IRepository<>)).ToProvider(typeof (RepositoryProvider));
           // Bind<IRepository<HotListEntry>>().To<HotListEntriesRepository>();
            Bind(typeof (IRepository<>)).To(typeof(EfSingleIdRepository<>));
            Bind<IEntitySqlGeneratorsProvider>().To<EntitySqlGeneratorsProvider>();
            Bind<IDbContextProvider>().To<DbContextProvider>();
            Bind<IEntityPropertySelectionAnalyzer>().To<EntityPropertySelectionAnalyzer>();
            Bind<IEntityInfoResolver>().To<EntityInfoResolver>();
        }
    }
}
