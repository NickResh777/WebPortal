using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WebPortal.BusinessLogic.OnlineUsers {
    class OnlineUsersStorageConfigSection : ConfigurationSection {

        public int TimeDueTimeSeconds{
            get{
                return (int) this["timerDueTime"];
            }
        }

        public int EntryLifetimeInSeconds{
            get{
                return (int) this["entryLifetimePeriod"];
            }
        }
    }
}
