using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace WebPortal.BusinessLogic.Security {
    [Serializable]
    public class WebPortalMemberIdentity : IIdentity {

        /// <summary>
        /// ID of the current member
        /// </summary>
        public int MemberId{
            get; 
            set; 
        }

        /// <summary>
        /// 
        /// </summary>
        public string MemberNickname{
            get; 
            set; 
        }

        /// <summary>
        /// Is on trial??
        /// </summary>
        public bool MemberIsTrial{
            get; 
            set; 
        }


        public DateTime? ExpireDate{
            get; 
            set; 
        }



        public string AuthenticationType {
            get{
                return "Default";
            }
        }

        public bool IsAuthenticated {
            get{
                return (MemberId != 0);
            }
        }

        public string Name {
            get { throw new NotImplementedException(); }
        }
    }
}
