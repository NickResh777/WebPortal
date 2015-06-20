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
            StopTimerResetEvent();
          
            lock (_entriesLock){
                if (_store != null){
                        DateTime now = DateTime.Now;
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
            }

            // restart the timer and notify waiting threads that 
            // the expired entries have been deleted
            StartTimerSetEvent();
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
                      // if the current object is yet not disposed
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

        private void StopTimerResetEvent(){
            try{
                // set timer's wait & period time to infinity to stop it
                _timer.Change(dueTime: Timeout.Infinite,
                               period: Timeout.Infinite);
                _expiredEntriesRemovedEvent.Reset();
            } catch(ObjectDisposedException ex){
                // timer is probably disposed
                // release other waiting threas so
                // they could execute the last time
                _expiredEntriesRemovedEvent.Set();
            }
        }

        private void StartTimerSetEvent(){
            try{
                // set event so that waiting threads could continue execution 
                _expiredEntriesRemovedEvent.Set();
                // set timer due time to 15 secs so it would fire after 15 seconds
                _timer.Change(dueTime: TimeSpan.FromSeconds(_entryLifetimeInSeconds),
                               period: TimeSpan.FromSeconds(_entryLifetimeInSeconds));
            } catch (ObjectDisposedException ex){
                
            }
        }

        public void Dispose(){
             // wait until expired entries are deleted 
            _expiredEntriesRemovedEvent.WaitOne();

            lock (_entriesLock){
                    // disposing the timer
                   _timer.Dispose(_timerDisposedEvent);

                   // wait until the timer is disposed
                   _timerDisposedEvent.WaitOne();
                   // clear the cache
                   _store.Clear();
                   _store = null;

                  // dispose the event
                  _timerDisposedEvent.Close();
            }

            // CANNOT dispose the _expiredEntriesRemovedEvent
            // cause it would throw exception and to handle the exception
            // is performance critical issue
            // CANNOT use TRY here
        }
    }
}
