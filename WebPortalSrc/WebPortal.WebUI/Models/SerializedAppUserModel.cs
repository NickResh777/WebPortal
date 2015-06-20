using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPortal.WebUI.Models
{
    [Serializable]
    public class SerializedAppUserModel
    {
        /// <summary>
        /// ID to the AppUser entity
        /// </summary>
        public int AppUserId { get; set; }

        /// <summary>
        /// ID to the Member entity
        /// </summary>
        public int? MemberId { get; set; }
 
        /// <summary>
        /// Encrypted app user role by the AES alhorithm (128-bit) 
        /// </summary>
        public string EncryptedRole { get; set; }

        /// <summary>
        /// Name of the user 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Is user confirmed
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EncryptedIsTrialMembership { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? PaidMembershipExpiresOn { get; set; }
    }
}