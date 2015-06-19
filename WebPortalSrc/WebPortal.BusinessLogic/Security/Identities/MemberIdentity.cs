using System;
using System.Security.Principal;
using WebPortal.Entities.Authentication;

namespace WebPortal.BusinessLogic.Security.Identities
{
    public class MemberIdentity: INonGuestIdentity
    {
        public MemberIdentity(AppUser appUser){
            if (appUser == null){
                throw new NullReferenceException("appUser");
            }

            if ((appUser.Member == null) || (appUser.RefMemberId == null)){
                 // we
                 throw new InvalidOperationException("Associated link to <Member> was not defined");
            }

            AppUserId = appUser.Id;
            MemberId = appUser.RefMemberId.Value;
            IsTrialMembership = appUser.Member.IsTrial;
        }

        public string AuthenticationType
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsAuthenticated
        {
            get{
                // always true since the principal has this 
                // identity [MemberIdentity] which means
                // the principal's identity is yet non-guest
                return true;             
            }
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public int MemberId { get; private set; } 

        public string Email { get; private set; }

        public bool IsTrialMembership { get; set; }

        public DateTime? PaidFeaturesExpireOn { get; set; }

        
        public bool CanUsePaidFeatures{
            get{
                return !IsTrialMembership && 
                       PaidFeaturesExpireOn.HasValue && 
                       PaidFeaturesExpireOn.Value > DateTime.Now;
            }
        }

        public int AppUserId { get; private set; }
    }
}
