using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebPortal.Entities.Authentication;

namespace WebPortal.BusinessLogic.Services.Security
{
    public interface IFormsAuthenticationService{
        /// <summary>
        /// 
        /// </summary>
        AppUser LogIn(string userNameOrEmail, string password, bool isPersistentCookie);

        /// <summary>
        /// Log out from the web site
        /// </summary>
        void LogOut();
    }
}
