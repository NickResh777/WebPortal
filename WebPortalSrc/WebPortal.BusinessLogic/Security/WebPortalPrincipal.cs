using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace WebPortal.BusinessLogic.Security {
    public class WebPortalPrincipal : IPrincipal{

        private readonly WebPortalMemberIdentity _identity;

        public WebPortalPrincipal(WebPortalMemberIdentity identity){
            if (identity == null){
                // member identity is not defined
                throw new NullReferenceException("identity");
            }

            _identity = identity;
        }

        public bool CanUsePaidFeatures{
            get{
                return _identity.IsAuthenticated &&
                       !_identity.MemberIsTrial &&
                       _identity.ExpireDate != null &&
                       /*todo: fix date comparison */DateTime.Now < _identity.ExpireDate;
            }
        }

        


        public IIdentity Identity {
            get{
                return _identity;
            }
        }

        public bool IsInRole(string role){


            return false;
        }
    }
}
