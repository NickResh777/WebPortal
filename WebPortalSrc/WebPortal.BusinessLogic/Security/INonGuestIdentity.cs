using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace WebPortal.BusinessLogic.Security
{
    public interface INonGuestIdentity : IIdentity
    {
        /// <summary>
        /// Id of the application user stored in the users store
        /// </summary>
        int AppUserId { get;  }
    }
}
