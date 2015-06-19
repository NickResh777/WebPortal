using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities.Members;

namespace WebPortal.Entities.Authentication
{
    public class AppUser: BaseBusinessEntityWithId
    {
        /// <summary>
        /// Facebook, Twitter, Google+ or default
        /// </summary>
        public string AuthProvider { get; set; }

        /// <summary>
        /// Associated Member instance (for members)
        /// </summary>
        public int? RefMemberId { get; set; }

        /// <summary>
        /// Name of the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Role of the application user (admin, member)
        /// </summary>
        public int Role { get; set; }

        /// <summary>
        /// Use has confirmed his identity??
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PasswordHash { get; set; }

        // 
        public DateTime? LastLoggedInOn { get; set; }

        /// <summary>
        /// Associated Member entity (for members)
        /// </summary>
        public virtual Member Member { get; set; }
    }
}
