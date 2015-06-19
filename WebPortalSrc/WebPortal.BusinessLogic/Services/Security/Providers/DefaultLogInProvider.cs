using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Services.Security.Providers {
    class DefaultLogInProvider : ILogInProvider {

        public bool Authenticate(string userId, string password) {
            throw new NotImplementedException();
        }

        public string Name {
            get{
                return "WebPortalDefault";
            }
        }
    }
}
