using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using WebPortal.BusinessLogic.Security.Identities;

namespace WebPortal.BusinessLogic.Security {
    public class WebPortalPrincipal : IPrincipal{

        private readonly IIdentity _identity;
        private readonly string[] _roles;

        public WebPortalPrincipal(IIdentity identity, string[] roles){
            if (identity == null){
                  // member identity is not defined
                  throw new NullReferenceException("identity");
            }

            _identity = identity;
            _roles = roles;

            if (identity is INonGuestIdentity){
                // set <AppUserId> property for non-guest users
                AppUserId = ((INonGuestIdentity) identity).AppUserId;
            }
        }

        public WebPortalPrincipal(IIdentity identity, string role) : this(identity, new[]{role}){
            
        }

        /// <summary>
        /// Id of the AppUser entity
        /// Values is NULL if the principal is Guest
        /// </summary>
        public int? AppUserId { get; private set; }


        public bool IsAdmin{
            get{
                // Is admin?
                return IsInRole(Roles.ROLE_ADMIN) && (Identity is AdminIdentity);
            }
        }

        public bool IsMember{
            get{
                // Is member?
                return IsInRole(Roles.ROLE_MEMBER) && (Identity is MemberIdentity);
            }
        }

        public bool IsGuest{
            get{
                // check for role "guest" and 
                return IsInRole(Roles.ROLE_GUEST) && (Identity is GuestIdentity);
            }
        }

       
        public IIdentity Identity {
            get{
                return _identity;
            }
        }

        public bool IsInRole(string role){
           
            return (_roles != null) && _roles.Contains(role);
        }
    }
}
