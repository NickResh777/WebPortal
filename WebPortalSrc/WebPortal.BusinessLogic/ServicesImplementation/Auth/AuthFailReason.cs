using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.ServicesImplementation.Auth
{
    public enum AuthFailReason
    {
        NoError = 0,
        UserNotFoundByName,
        UserNotFoundByEmal,
        InvalidEmail,
        InvalidPassword
    }
}
