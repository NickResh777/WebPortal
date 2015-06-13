using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.OnlineUsers
{
    public class OnlineUserEntry
    {
        public int MemberId { get; set; }

        public string Email { get; set; }

        public DateTime OnlineSince { get; set; }
    }
}
