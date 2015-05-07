using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using WebPortal.Entities;
using Ninject.Infrastructure.Language;

namespace WebPortal.DataAccessLayer.EntityAdapters {
    class EntityEnumerator<T, TEnt>: IEnumerator<T> where T: IEntityAdapter, new() where TEnt: BaseEntity{
        private readonly DbSet<TEnt> _dbSet;
        private IEnumerable<TEnt> _enumerable;
        private IEnumerator<TEnt> _enumerator; 

        public EntityEnumerator(DbSet<TEnt> dbSet){
            if (dbSet == null){
                throw new NullReferenceException("dbSet");
            }

            _dbSet = dbSet;
        }

        private IEnumerable<TEnt> GetEntityEnumerable(){
            return _enumerable ?? (_enumerable = _dbSet.ToEnumerable());
        }

        private IEnumerator<TEnt> GetEntityEnumerator(){
            if (_enumerator == null){
                var enumerable = GetEntityEnumerable();
                _enumerator = enumerable.GetEnumerator();
            }

            return _enumerator;
        } 

        public T Current {
            get{
                var currentEntity = GetEntityEnumerator().Current;
                return new T{
                    Entity = currentEntity
                };
            }
        }

        public void Dispose() {
           
        }

        object System.Collections.IEnumerator.Current {
            get{
                var currentEntity = GetEntityEnumerator().Current;
                return new T{
                    Entity = currentEntity
                };
            }
        }

        public bool MoveNext(){
            // make a move forward in the underlying DbSet
            return GetEntityEnumerator().MoveNext();
        }

        public void Reset() {
            
            GetEntityEnumerator().Reset();
        }
    }
}
