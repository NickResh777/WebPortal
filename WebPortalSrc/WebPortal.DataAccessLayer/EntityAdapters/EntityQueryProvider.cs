using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.EntityAdapters {
    class EntityQueryProvider<TEnt, TAdapter> : IQueryProvider 
        where TEnt : BaseEntity where TAdapter: class, IEntityAdapter, new(){
        private readonly DbSet<TEnt> _entitySet;

        public EntityQueryProvider(DbSet<TEnt> entitySet){
            _entitySet = entitySet;
        }



        public IQueryable CreateQuery(Expression expression){
            return null;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression){
            if (typeof (TElement) != typeof (TAdapter)){
                //
                throw new Exception("Must create adapters query");
            }

          

            // cast to the IQueryable<TElement> 
            return null;
        }

        public TResult Execute<TResult>(Expression expression) {
            throw new NotImplementedException();
        }

        public object Execute(Expression expression) {
            throw new NotImplementedException();
        }
    }
}
