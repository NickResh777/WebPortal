using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace WebPortal.BusinessLogic.OnlineUsers
{
    public class OnlineUsersStorage
    {
        private readonly object _entriesLock = new object();
       // private readonly MemoryCache _cache = MemoryCache.Default;
        private const string CacheKeyFormat = "online_{0}";

        private readonly Hashtable _cacheOnline;  
        private readonly ManualResetEvent _removingExpiredEvent;
        private Timer _timer;
        private int _onlineEntryLifetimeSeconds;
        private int _timerDueTimeSeconds;

     

        public OnlineUsersStorage(){
            SetTimeConfiguratedValues();
            _cacheOnline = new Hashtable();
            _removingExpiredEvent = new ManualResetEvent(false);
            _timer = new Timer(callback: RemoveExpiredEntriesCallback,
                               state: null,
                               dueTime: TimeSpan.FromSeconds(30.00), 
                               period: TimeSpan.FromSeconds(_onlineEntryLifetimeSeconds)
             );
        }

        private void SetTimeConfiguratedValues(){
                  ConfigurationManager
        }

        private void RemoveExpiredEntriesCallback(object state){
            // block other threads
            _removingExpiredEvent.Reset();

            var entiesToRemoveList = new List<OnlineUserEntry>();

            foreach (var entryKey in _cacheOnline.Keys){
               OnlineUserEntry entry = (OnlineUserEntry) _cacheOnline[entryKey];
               if (IsExpired(entry)){
                  // remove entry if it exceeded its lifetime
                  entiesToRemoveList.Add(entry);
               }
            }

            if (entiesToRemoveList.Any()){
                 // remove the expired entries
                 entiesToRemoveList.ForEach( ouEntry => _cacheOnline.Remove(ouEntry));
            }

            // notify other threads
            _removingExpiredEvent.Set();
        }

        public void SetUserOnline(OnlineUserEntry entry){
            // wait until all expired entries are deleted
            _removingExpiredEvent.WaitOne();

            lock (_entriesLock){
                   string cacheKey = string.Format(CacheKeyFormat, entry.MemberId);
                   _cacheOnline[entry.MemberId] = entry;
            }
        }

        public bool IsOnline(OnlineUserEntry entry){
            // wait until all expired entries are deleted
            _removingExpiredEvent.WaitOne();

            lock (_entriesLock){
                string cacheKey = string.Format(CacheKeyFormat, entry.MemberId);
                OnlineUserEntry uEntry = (OnlineUserEntry)_cacheOnline[entry.MemberId];
                return (uEntry != null) && !IsExpired(uEntry);
            }
        }

        private bool IsExpired(OnlineUserEntry entry){
            DateTime expireDt = entry.OnlineSince.AddSeconds(_onlineEntryLifetimeSeconds);
            return expireDt < DateTime.Now;
        }
    }
}
