using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.BusinessLogic.Security;

namespace WebPortal.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public WebPortalPrincipal PortalUser{
            get{
                // 
                return (User as WebPortalPrincipal);
            }
        }
    }
}
