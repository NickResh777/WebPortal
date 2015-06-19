using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities.Authentication;

namespace WebPortal.BusinessLogic.ServicesImplementation.Auth {
    public class AuthenticationResult {
        /// <summary>
        /// Did the user manage to log in? 
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// Additional information on why the log-in failed for user
        /// </summary>
        public LogInFailReason FailReason { get; set; }

        /// <summary>
        /// Get the user that was successful logged in 
        /// </summary>
        public AppUser AuthenticatedUser { get; set; }
    }
}
