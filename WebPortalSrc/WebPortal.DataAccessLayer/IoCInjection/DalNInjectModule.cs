using Ninject.Modules;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;


namespace WebPortal.DataAccessLayer.IoCInjection {
    public class DalNInjectModule: NinjectModule {
        public override void Load(){
            // always create a new repository
            Bind<IDbContext>().To<WebPortalDbContext>().InTransientScope();
            Bind(typeof (IRepository<>)).To(typeof (EfRepository<>));
            Bind<IEntityPropertySelectionAnalyzer>().To<EntityPropertySelectionAnalyzer>();
            Bind<IEntityInfoResolver>().To<EntityInfoResolver>();
        }
    }
}
