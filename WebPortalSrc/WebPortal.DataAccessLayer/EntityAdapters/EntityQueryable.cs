using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.EntityAdapters {
    class EntityQueryable<TEnt, TAdapter> : IQueryable<TAdapter> 
          where TAdapter: IEntityAdapter, new() where TEnt: BaseEntity{
        private readonly DbSet<TEnt> _entitySet;
        private readonly EntityEnumerator<TAdapter, TEnt> _enumerator; 


        public EntityQueryable(DbSet<TEnt> entitySet){
            _entitySet = entitySet;
            _enumerator = new EntityEnumerator<TAdapter, TEnt>( entitySet);
        }


        public IEnumerator<TAdapter> GetEnumerator(){
            return _enumerator;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator(){
            return _enumerator;
        }

        public Type ElementType {
            get{
                // here we deall with adapters 
                return typeof (TAdapter);
            }
        }

        public System.Linq.Expressions.Expression Expression {
            get { throw new NotImplementedException(); }
        }

        public IQueryProvider Provider {
            get{
                
                throw new NotImplementedException();
            }
        }
    }
}
