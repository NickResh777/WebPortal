using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities.Authentication;

namespace WebPortal.BusinessLogic.ServicesImplementation.Auth {
    public class AuthenticationResult {
        public AuthenticationResult(bool isAuthenticated,
                                    AuthFailReason failReason,
                                    AppUser appUser){
            IsSuccess = isAuthenticated;
            FailReason = failReason;
            AuthenticatedUser = appUser;
        }

        /// <summary>
        /// Did the user manage to log in? 
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Additional information on why the log-in failed for user
        /// </summary>
        public AuthFailReason FailReason { get; private set; }

        /// <summary>
        /// Get the user that was successful logged in 
        /// </summary>
        public AppUser AuthenticatedUser { get; private set; }


        public bool IsAuthenticated{
            get{
                return IsSuccess &&
                       (AuthenticatedUser != null) &&
                       (FailReason == AuthFailReason.NoError);
            }
        }
    }
}
