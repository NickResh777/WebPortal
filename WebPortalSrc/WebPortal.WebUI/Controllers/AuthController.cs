using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.BusinessLogic.Services;
using WebPortal.BusinessLogic.Services.Security;

namespace WebPortal.WebUI.Controllers
{
    public class AuthController : BaseController{
        private readonly IAuthenticationService _authService;

        public ActionResult Index()
        {   
            return View();
        }

    }
}
