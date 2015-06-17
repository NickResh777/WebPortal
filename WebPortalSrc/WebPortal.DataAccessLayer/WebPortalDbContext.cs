using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;
using WebPortal.DataAccessLayer.Infrastructure;
using WebPortal.DataAccessLayer.Mapping;
using WebPortal.DataAccessLayer.Mapping.Geo;
using WebPortal.Entities;
using WebPortal.Entities.Geo;
using WebPortal.Entities.Members;
using WebPortal.Entities.Profile;


namespace WebPortal.DataAccessLayer {
    public class WebPortalDbContext : DbContext, IDbContext{
        public WebPortalDbContext(){

            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){
           // register entity types
           RegisterEntityTypeConfigurations(modelBuilder);

           // fix error with DateTime conversion to the DB type
           HandleDateTimeError(modelBuilder); 
        }

        private void HandleDateTimeError(DbModelBuilder modelBuilder){
            modelBuilder.Properties<DateTime>().Configure(
                config => config.HasColumnType("datetime2").
                                 HasPrecision(0)
            );
        }

        private void RegisterEntityTypeConfigurations(DbModelBuilder modelBuilder){
            var entityConfigTypes = (from type in typeof (WebPortalDbContext).Assembly.GetTypes()
                where type.IsClass && !type.IsAbstract &&
                      (type.BaseType != null) &&
                      type.BaseType.IsGenericType &&
                      type.BaseType.GetGenericTypeDefinition() == typeof (EntityTypeConfiguration<>)
                select type).ToList();

            entityConfigTypes.ForEach(entConfigType => {
                dynamic entityConfigInstance = Activator.CreateInstance(entConfigType);
                modelBuilder.Configurations.Add(entityConfigInstance);
            });
        }

        public DbSet<T> Set<T>() where T : class {
            return base.Set<T>();
        }


        /// <summary>
        /// Get ObjectContext from the current DbContext
        /// </summary>
        public ObjectContext ObjectContext{
            get{
                return ((IObjectContextAdapter) this).ObjectContext;
            }
        }


        public void MarkAsChanged(object entity)
        {
            if (entity == null){
                throw new NullReferenceException("entity");
            }

           Entry(entity).State = EntityState.Modified;
        }

        
        public void MarkAsDeleted(object entity)
        {
            if (entity == null){
                  throw new NullReferenceException("entity");
            }

            Entry(entity).State = EntityState.Detached;
        }
    }
}
