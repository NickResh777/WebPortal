using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.BusinessLogic.ServicesImplementation.Auth;

namespace WebPortal.BusinessLogic.Services {
    public interface IAuthenticationService{
        LogInResult LogInViaNickName(string nickName, string password);

        LogInResult LogInViaEmail(string email, string password);

        void LogOut();
    }
}
