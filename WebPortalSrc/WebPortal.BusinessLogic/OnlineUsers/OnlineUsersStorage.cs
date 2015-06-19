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
    public class OnlineUsersStorage: IDisposable
    {
        private readonly object _entriesLock = new object();
        private          Hashtable _store;  
        private readonly ManualResetEvent _expiredEntriesRemovedEvent;
        private readonly ManualResetEvent _timerDisposedEvent;
        private readonly Timer _timer;
        private int _entryLifetimeInSeconds;
        private int _timerDueTimeInSeconds;

        private const int DEFAULT_TIMER_DUE_TIME = 30;
        private const int DEFAULT_ENTRY_LIFETIME = 15; // 15 seconds

     

        public OnlineUsersStorage(){
            SetTimeConfiguratedValues();
            _store = new Hashtable();
            _expiredEntriesRemovedEvent = new ManualResetEvent(true);
            _timerDisposedEvent = new ManualResetEvent(true);
            _timer = new Timer(callback: RemoveExpiredEntriesCallback,
                               state: null,
                               dueTime: TimeSpan.FromSeconds(_timerDueTimeInSeconds), 
                               period: TimeSpan.FromSeconds(_entryLifetimeInSeconds)
             );
        }

        private void SetTimeConfiguratedValues(){
            try{
                var config = (OnlineUsersStorageConfigSection) ConfigurationManager.GetSection("onlineUsersStorage");
                _timerDueTimeInSeconds = config.TimeDueTimeSeconds;
                _entryLifetimeInSeconds = config.EntryLifetimeInSeconds;
            } catch{
                _timerDueTimeInSeconds = DEFAULT_TIMER_DUE_TIME;
                _entryLifetimeInSeconds = DEFAULT_ENTRY_LIFETIME;
            }   
        }

        private void RemoveExpiredEntriesCallback(object state){
            // stop the timer
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            // block other threads
            _expiredEntriesRemovedEvent.Reset();

            DateTime now = DateTime.Now;

            lock (_entriesLock){
                    // if not yet disposed
                    var expiredEntriesList = new List<OnlineUserEntry>();

                    foreach (var entryKey in _store.Keys){
                        OnlineUserEntry entry = (OnlineUserEntry) _store[entryKey];
                        if (IsExpired(entry, now)){
                            // remove entry if it exceeded its lifetime
                            expiredEntriesList.Add(entry);
                        }
                    }

                    if (expiredEntriesList.Any()){
                        // remove the expired entries
                        expiredEntriesList.ForEach(ouEntry => _store.Remove(ouEntry));
                    }
            }

            // notify other threads
            _expiredEntriesRemovedEvent.Set();
            // restart the timer 
            _timer.Change(TimeSpan.FromMilliseconds(0.00), TimeSpan.FromSeconds(_entryLifetimeInSeconds));
        }

        public void SetUserOnline(OnlineUserEntry entry){
            // wait until all expired entries are deleted
            _expiredEntriesRemovedEvent.WaitOne();

            lock (_entriesLock){
                // add or reset user as online 
                if (_store != null){
                    // if not yet disposed
                    entry.OnlineSince = DateTime.Now;
                    _store[entry.MemberId] = entry;
                }
            }
        }

        public bool IsOnline(OnlineUserEntry entry){
            // wait until all expired entries are deleted
            _expiredEntriesRemovedEvent.WaitOne();

            lock (_entriesLock){
                if (_store != null){
                      // this object is not disposed
                      DateTime now = DateTime.Now;
                      OnlineUserEntry uEntry = (OnlineUserEntry) _store[entry.MemberId];
                      return (uEntry != null) && !IsExpired(uEntry, now);
                }
                // if the object is disposed, then return FALSE
                return false;
            }
        }

        public bool IsOnline(int memberId){
            // wait until all expired entries are deleted
            _expiredEntriesRemovedEvent.WaitOne();

            lock (_entriesLock){
                if (_store != null){
                      // if not yet disposed
                      DateTime now = DateTime.Now;
                      OnlineUserEntry uEntry = (OnlineUserEntry) _store[memberId];
                      return (uEntry != null) && !IsExpired(uEntry, now);
                }
                // in case of object disposed return false 
                return false;
            }
        }

        private bool IsExpired(OnlineUserEntry entry, DateTime nowTime){
            DateTime expireTime = entry.OnlineSince.AddSeconds(_entryLifetimeInSeconds);
            return (expireTime < nowTime);
        }

        public void Dispose(){
             // wait until expired entries are deleted 
            _expiredEntriesRemovedEvent.WaitOne();

            // block other threads waiting in the start of method execution
            _expiredEntriesRemovedEvent.Reset();

            lock (_entriesLock){
                  // disposing the timer
                  _timer.Dispose(_timerDisposedEvent);
                  //wait until timer is disposed
                  _timerDisposedEvent.WaitOne();

                   // clear the cache
                   _store.Clear();
                   _store = null;
                   // dispose the timer event of dispose
                   _timerDisposedEvent.Close();

                   _expiredEntriesRemovedEvent.Set();
            }

            // CANNOT dispose the _expiredEntriesRemovedEvent
            // cause 
        }

        private void WaitForExpiredEntriesRemovedEvent(){
            
        }
    }
}
