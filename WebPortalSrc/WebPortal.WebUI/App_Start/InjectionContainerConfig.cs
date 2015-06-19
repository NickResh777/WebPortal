using DatingHeaven.Core;
using Ninject.Modules;
using WebPortal.BusinessLogic;
using WebPortal.DataAccessLayer.IoCInjection;

namespace WebPortal.WebUI
{
    public static class InjectionContainerConfig
    {
        public static void Register(){
            INinjectModule[] modules = {
                new DalNInjectModule(),
                new ServicesNInjectModule()
            };

            GlobalInjectionContainer.Instance.LoadModules(modules);
        }
    }
}