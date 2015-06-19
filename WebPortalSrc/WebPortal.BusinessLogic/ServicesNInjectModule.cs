using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.BusinessLogic.Services;
using WebPortal.BusinessLogic.ServicesImplementation;
using Ninject.Modules;

namespace WebPortal.BusinessLogic {
    public class ServicesNInjectModule: NinjectModule {
        public override void Load(){
            Bind<IHotListService>().To<HotListService>();
        }
    }
}
