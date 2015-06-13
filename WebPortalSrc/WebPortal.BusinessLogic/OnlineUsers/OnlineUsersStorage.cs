using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace WebPortal.BusinessLogic.OnlineUsers
{
    public class OnlineUsersStorage
    {
        private readonly object _syncRoot = new object();
        private readonly List<OnlineUserEntry> _usersList = new List<OnlineUserEntry>();

        public OnlineUsersStorage()
        {
           
        }


        public void SetUserOnline(OnlineUserEntry entry)
        {
            lock (_syncRoot){
                // try to add
                _usersList.Add(entry);
            }
        }

        public void SetExpiredEntriesOffline()
        {
            lock (_syncRoot){
                // remove expired entries
                _usersList.RemoveAll(ou => ou.OnlineSince.AddMilliseconds(10000) < DateTime.Now);
            }
        }

        public OnlineUserEntry[] GetOnline()
        {
            List<OnlineUserEntry> result = null;
            lock (_syncRoot){
                var onlineUsersQuery = from ou in _usersList
                                       where (ou.OnlineSince.AddMilliseconds(10000) >= DateTime.Now)
                                       select ou;
                result = onlineUsersQuery.ToList();
            }
              
            // remove duplicates
          
            return result.ToArray();
        }
    }
}
