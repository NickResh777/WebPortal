using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebPortal.Entities.Members;

namespace WebPortal.DataAccessLayer.Mapping {
    class MemberMap: BaseBusinessEntityWithIdMap<Member>{
        public MemberMap(){

            ToTable("Members");
            Property(m => m.FirstName).
                HasColumnName("FirstName").
                IsOptional().
                HasMaxLength(100);

            Property(m => m.LastName).
                HasColumnName("LastName").
                HasMaxLength(100).
                IsOptional();

            Property(m => m.LastVisit).IsOptional().HasColumnName("LastVisit");

            Property(m => m.Gender).IsRequired().HasColumnName("Gender").HasMaxLength(1).HasColumnType("char");
            Property(m => m.NickName).IsRequired().HasMaxLength(50).HasColumnName("NickName");
            Property(m => m.Email).IsRequired().HasMaxLength(200).HasColumnName("Email");

            HasRequired(m => m.Profile).WithRequiredPrincipal();

           
        }
    }
}
