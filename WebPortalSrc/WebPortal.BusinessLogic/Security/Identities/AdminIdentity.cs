using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using WebPortal.Entities.Authentication;

namespace WebPortal.BusinessLogic.Security.Identities
{
    public class AdminIdentity : INonGuestIdentity
    {
        public AdminIdentity(AppUser appUser){
            if (appUser == null){
                  throw new NullReferenceException("appUser");
            }

            AppUserId = appUser.Id;
        }

        public AdminIdentity(int appUserId){
            // application user id
            AppUserId = appUserId;
        }


        public string AuthenticationType
        {
            get{
                return "Default";
            }
        }

        public bool IsAuthenticated
        {
            get{
                return true;
            }
        }

        public string Name
        {
            get{
                return "";
            }
        }

        public int AppUserId { get; private set; }
    }
}
