using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer {
    public interface IDbContext : IDisposable{
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        DbSet<T> Set<T>() where T: class;


        DbEntityEntry Entry(object entity);

        /// <summary>
        /// 
        /// </summary>
        Database Database { get; }

        /// <summary>
        /// Get 
        /// </summary>
        ObjectContext ObjectContext { get; }

        /// <summary>
        /// Save changes pending in the context
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// Mark entity as CHANGED in the context
        /// </summary>
        void MarkAsChanged(object entity);

        /// <summary>
        /// Mark entity as DELETED in the context
        /// </summary>
        void MarkAsDeleted(object entity);
    }
}
