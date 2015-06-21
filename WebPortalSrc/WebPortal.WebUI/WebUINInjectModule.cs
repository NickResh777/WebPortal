using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Ninject.Modules;
using System.Reflection;
using WebPortal.BusinessLogic.Security;
using WebPortal.BusinessLogic.Services.Security.Providers;
using WebPortal.WebUI.Controllers;

namespace WebPortal.WebUI
{
    public class WebUiNInjectModule : NinjectModule
    {
        public override void Load(){
            var controllerTypes = (from type in Assembly.GetAssembly(typeof (BaseController)).GetTypes()
                                   where typeof (IController).IsAssignableFrom(type)
                                   select type).ToList();

            if (controllerTypes.Any()){
                 // register all MVC controllers
                 controllerTypes.ForEach(ct => Bind(ct).ToSelf().InTransientScope());
            }

            Bind<IEncryptionProvider>().To<EncryptionProvider>();
        }
    }
}