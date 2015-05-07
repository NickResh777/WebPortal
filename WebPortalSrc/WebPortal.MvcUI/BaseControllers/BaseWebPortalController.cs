using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.BusinessLogic.Security;

namespace WebPortal.MvcUI.BaseControllers {
    public class BaseWebPortalController : Controller {

        public new WebPortalPrincipal User{
            get{
                WebPortalPrincipal principal = (WebPortalPrincipal) base.User;
                return principal;
            }
        } 
    }
}