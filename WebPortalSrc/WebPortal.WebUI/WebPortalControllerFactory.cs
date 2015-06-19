using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DatingHeaven.Core;

namespace WebPortal.WebUI
{
    public class WebPortalControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType){
            return (controllerType != null)
                ? (IController) GlobalInjectionContainer.Instance.Get(controllerType)
                : null;
        }
    }
}