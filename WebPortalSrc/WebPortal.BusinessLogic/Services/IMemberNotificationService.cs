using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.Services
{
    public interface IMemberNotificationService{
        /// <summary>
        /// Notify member of him been added to the hot list of another member
        /// </summary>
        /// <param name="member"></param>
        /// <param name="targetMember"></param>
        /// <param name="comment">Comment when adding a member</param>
        void NotifyAddedToHotList(int member, int targetMember, string comment);

        void NotifyReceivedMessage(int member);
    }
}
