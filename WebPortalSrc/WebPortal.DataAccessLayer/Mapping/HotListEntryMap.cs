using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using WebPortal.Entities;

namespace WebPortal.DataAccessLayer.Mapping {
    public class HotListEntryMap : EntityTypeConfiguration<HotListEntry>{
        public HotListEntryMap(){
            ToTable("HotList");

            // the composite key consists of the member who adds other member (target) to 
            // his hot list
            HasKey(entry => new{
                entry.MemberId,
                entry.TargetMemberId
            });

            Property(entry => entry.MemberId)
                .HasColumnName("MemberId")
                .HasColumnOrder(0);

            Property(entry => entry.TargetMemberId)
                .HasColumnName("TargetMemberId")
                .HasColumnOrder(1);

           
            Property(entry => entry.ShouldNotify)
                .IsRequired();

            Property(entry => entry.Comment)
                .IsOptional()
                .HasMaxLength(500);



            HasRequired(e => e.CurrentMember)
                .WithMany()
                .HasForeignKey(e => e.MemberId)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.TargetMember)
                .WithMany()
                .HasForeignKey(e => e.TargetMemberId)
                .WillCascadeOnDelete(false);

        }
    }
}
