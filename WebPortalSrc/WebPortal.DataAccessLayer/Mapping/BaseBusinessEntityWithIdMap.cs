using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Mapping {
    abstract class BaseBusinessEntityWithIdMap<T> : EntityTypeConfiguration<T> 
        where T : BaseBusinessEntityWithId{
        protected BaseBusinessEntityWithIdMap(){
            // mark property 'Id' as the entity key
            HasKey(entity => entity.Id);

            // mark property 'Id' as the identity column
            Property(entity => entity.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        } 
    }
}
