using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using WebPortal.DataAccessLayer.FluentSyntax;
using WebPortal.DataAccessLayer.Infrastructure.EntityOperations.SqlGenerators;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer {
    public interface IRepository<T> where T: BaseEntity{
        /// <summary>
        /// Get entity by key
        /// </summary>
        T GetById(object entityKey, params Expression<Func<T, object>>[] includedProperties);

        /// <summary>
        /// Get all entities in the table
        /// </summary>
        IList<T> GetAll(params Expression<Func<T, object>>[] incProps);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="incProps"></param>
        /// <returns></returns>
        IList<T> GetWhere(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] incProps); 

        /// <summary>
        /// Get total amount of entities stored in the table
        /// </summary>
        int GetCount();

        /// <summary>
        /// Get count of entities that fit the current filter
        /// </summary>
        int GetCountWhere(Expression<Func<T, bool>> predictate);

        /// <summary>
        /// Fluent syntax for filtering
        ///  _repo.Where().Property(m => m.SenderId).Equals(32).And().
        ///                Property(m => m.IsRead).Equals(false).And().
        ///                Property(m => m.ReadOn).IsNull().Select()
        /// 
        ///  _repo.Where().Property(m => m.Login).Equals("KinkyLover007").And().
        ///                Property(m => m.IsTrial).Equals(false).Delete();
        /// 
        ///  _repo.Where().Property( m => m.Login).Equals("AwesomeBoy").Update( m => m.FirstName, "James");
        /// </summary>
        FluentSyntaxForWhereConditions<T> Where();

        /// <summary>
        /// Update the current entity
        /// </summary>
        void Refresh(T entity);

        /// <summary>
        /// Insert a new entity to the table 
        /// </summary>
        void Insert(T entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Delete entity by its key
        /// </summary>
        void DeleteById(object entityKey);

        /// <summary>
        /// Get the entity table 
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveSetChanges();
    }
}
