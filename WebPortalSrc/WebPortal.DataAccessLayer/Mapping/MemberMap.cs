using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using WebPortal.Entities.Members;

namespace WebPortal.DataAccessLayer.Mapping {
    class MemberMap: EntityTypeConfiguration<Member>{
        public MemberMap(){

            ToTable("Members");

            HasKey(m => m.Id);

            Property(m => m.Id).
                HasColumnName("Id").
                HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // first name
            Property(m => m.FirstName).
                HasColumnName("FirstName").
                IsOptional().
                HasMaxLength(50);

            Property(m => m.LastName).
                HasColumnName("LastName").
                IsOptional().
                HasMaxLength(100);

            Property(m => m.NickName).
                HasColumnName("Nickname").
                IsRequired().
                HasMaxLength(50);


            Property(m => m.Email).
                HasColumnName("Email").
                IsRequired().
                HasMaxLength(200);

            Property(m => m.LastVisit).
                HasColumnName("LastVisit").
                IsOptional();

            Property(m => m.Gender).
                IsRequired().
                HasColumnName("Gender").
                HasMaxLength(1).
                HasColumnType("char");

            
            

             HasRequired(m => m.Profile).WithRequiredPrincipal();        
        }
    }
}
