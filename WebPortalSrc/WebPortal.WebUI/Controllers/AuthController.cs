using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebPortal.BusinessLogic.Services;
using WebPortal.BusinessLogic.Services.Security;
using WebPortal.BusinessLogic.ServicesImplementation.Auth;
using WebPortal.Entities.Authentication;
using WebPortal.WebUI.Models;

namespace WebPortal.WebUI.Controllers
{
    public class AuthController : BaseController{
        private readonly IAuthenticationService _authService;
        private readonly Regex _emailRegex;

        public AuthController(IAuthenticationService authService){
            _authService = authService;
        }


        [HttpGet]
        [ActionName("login")]
        public ActionResult LogIn(){

            return View();
        }

        [HttpPost]
        [ActionName("login")]
        public ActionResult LogIn(LogInModel loginModel){
            AuthenticationResult logInResult;
            logInResult = _authService.AuthenticateUserByEmail(loginModel.Name, loginModel.Password);
            if (logInResult.IsAuthenticated && (logInResult.AuthenticatedUser != null)){
                // save auth cookie
                SaveAppUserInAuthCookie(logInResult.AuthenticatedUser);    
            }

            return View();
        }

        private void SaveAppUserInAuthCookie(AppUser appUser){
            
        }
    }
}
