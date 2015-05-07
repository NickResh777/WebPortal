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
        /// 
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
