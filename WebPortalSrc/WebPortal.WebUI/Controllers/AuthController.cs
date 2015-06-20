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
using WebPortal.DataAccessLayer;
using WebPortal.Entities.Authentication;
using WebPortal.WebUI.Models;

namespace WebPortal.WebUI.Controllers
{
    public class AuthController : BaseController{
        private readonly IApplicationUserService _authService;
        private readonly IRepository<AppUser> _repoAppUsers; 
        private readonly Regex _emailRegex;


        public AuthController(IApplicationUserService authService,
                              IRepository<AppUser> appUsersRepository){
            _authService = authService;
            _repoAppUsers = appUsersRepository;
        }


        [HttpGet]
        [ActionName("login")]
        public ActionResult LogIn(){

            return View();
        }

        [HttpPost]
        [ActionName("login")]
        public ActionResult LogIn(LogInModel logInModel){
            
            

            return View();
        }

        

       

        private void UpdateLastLoggedInProperty(AppUser appUser){
            appUser.LastLoggedInOn = DateTime.Now;
            _repoAppUsers.Refresh(appUser);
            _repoAppUsers.SaveSetChanges();
        }

       

     
    }
}
