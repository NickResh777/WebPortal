using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace WebPortal.BusinessLogic.OnlineUsers
{
    public class OnlineUsersStorage
    {
        private readonly object _expirationSyncLock = new object();
        private readonly List<OnlineUserEntry> _usersList = new List<OnlineUserEntry>();
        private const int OnlinePeriodMs = 10000;

        public void SetUserOnline(OnlineUserEntry entry)
        {
            lock (_expirationSyncLock){
                // try to add
                _usersList.Add(entry);
            }
        }

        public void SetExpiredEntriesOffline()
        {
            lock (_expirationSyncLock){
                // remove expired entries
                _usersList.RemoveAll(ou => ou.OnlineSince.AddMilliseconds(OnlinePeriodMs) < DateTime.Now);
            }
        }

        

        public OnlineUserEntry[] GetOnline()
        {
            List<OnlineUserEntry> result = null;
            lock (_expirationSyncLock){
                var onlineUsersQuery = from ou in _usersList
                                       where (ou.OnlineSince.AddMilliseconds(OnlinePeriodMs) >= DateTime.Now)
                                       select ou;
                result = onlineUsersQuery.ToList();
            }
              
            // remove duplicates
            var noDuplicatesQuery = from ou in result
                                    group ou by ou.MemberId into g
                                    select g.OrderByDescending(ouInGroup => ouInGroup.OnlineSince)
                                            .First();


            return noDuplicatesQuery.ToArray();
        }
    }
}
