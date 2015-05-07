using System;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Infrastructure.EntityOperations {
    public interface IEntityInfoResolver{
        /// <summary>
        /// Get name of the table by its entity type
        /// </summary>
        string GetTableName<T>() where T : class ;

        /// <summary>
        /// Get schema of the table by its entity type
        /// </summary>
        string GetTableSchema<T>() where T: class ;

        /// <summary>
        /// Get type of the entity property using its name
        /// </summary>
        Type GetPropertyType<T>(string property) where T : class ;


        /// <summary>
        /// Get the Key property of entity, having [KeyAttribute] 
        /// </summary>
        string GetEntityKeyProperty<T>() where T : class ;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        string[] GetOrderedKeyPropertyNames<T>() where T : BaseEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyPropertyOrder"></param>
        /// <returns></returns>
        string GetEntityKeyPropertyName<T>(int keyPropertyOrder) where T : BaseEntity;
    }
}
