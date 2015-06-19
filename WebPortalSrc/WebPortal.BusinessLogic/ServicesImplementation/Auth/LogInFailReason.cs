using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.ServicesImplementation.Auth
{
    public enum LogInFailReason
    {
        NoError = 0,
        UserByNameNotFound,
        InvalidEmail,
        InvalidPassword
    }
}
