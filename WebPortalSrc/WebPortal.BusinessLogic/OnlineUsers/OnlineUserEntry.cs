using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.OnlineUsers
{
    public class OnlineUserEntry : IComparable<OnlineUserEntry>
    {
        public int MemberId { get; set; }

        public string Email { get; set; }

        public DateTime OnlineSince { get; set; }

        public int CompareTo(OnlineUserEntry other)
        {
            int comparisonResult = 0;

            if (OnlineSince > other.OnlineSince)
                comparisonResult = (1);
            else if (OnlineSince < other.OnlineSince)
                comparisonResult = (-1);

            return comparisonResult;
        }
    }
}
