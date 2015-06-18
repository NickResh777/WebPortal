using System.Security.Principal;

namespace WebPortal.BusinessLogic.Security.Identities
{
    public class GuestIdentity : IIdentity
    {
        public string AuthenticationType
        {
            get{
                return "Default";
            }
        }

        public bool IsAuthenticated
        {
            get{
                return false;
            }
        }

        public string Name
        {
            get{
                return "guest";
            }
        }
    }
}
